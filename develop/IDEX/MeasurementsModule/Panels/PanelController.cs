// /*
//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 21.02.2017
//  */

namespace MeasurementsModule.Panels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Data;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Enum;
    using De.VwAudi.Idex.Common.Gui.Data;
    using De.VwAudi.Idex.Common.Gui.Util;
    using DevExpress.XtraBars.Ribbon;
    using KWP;
    using UDS;
    using EventHandler = Events.EventHandler;

    /// <summary>
    /// Klasse zum Steuern der UserControls
    /// </summary>
    internal sealed class PanelController
    {
        /// <summary>
        /// Index des UserControls MeasurementResults
        /// </summary>
        private const int INDEX_OF_MEASUREMENT_RESULTS = 1;

        /// <summary>
        /// Aktives UserControl
        /// </summary>
        private readonly AbstractPanelController _activePanelController;

        /// <summary>
        /// UserControl, welche die gepollten Messwerte anzeigt.
        /// </summary>
        private readonly MeasurementResults _measurementResults;
        
        /// <summary>
        /// Delegat zum hinzufügen von Ribbon-Pages-Gruppen zu dem Hauptribbon
        /// </summary>
        private readonly Action<RibbonPageGroup[]> _addRibbonGroupsDelegate;

        /// <summary>
        /// Initialisiert das richtige UserControl anhand des Protokolles
        /// </summary>
        /// <param name="aProtocol">Protocol um den Typ zu ermitteln.</param>
        /// <param name="aAddRibbonGroupsDelegate">Delegat zum hinzufügen von Ribbon-Pages-Gruppen zu dem Hauptribbon</param>
        internal PanelController(Protocol aProtocol, Action<RibbonPageGroup[]> aAddRibbonGroupsDelegate)
        {
            var protocol = aProtocol;
            _addRibbonGroupsDelegate = aAddRibbonGroupsDelegate;

            switch (protocol)
            {
                case Protocol.UdsOnCan:
                    _activePanelController = new UdsPanelController(aAddRibbonGroupsDelegate);
                    break;
                case Protocol.Kwp1281OnKline:
                case Protocol.Kwp1281OnTp16:
                case Protocol.Kwp2000OnTp20Can:
                case Protocol.Kwp2000OnTp16:
                case Protocol.Kwp2000OnKline:
                    _activePanelController = new KwpPanelController(aAddRibbonGroupsDelegate);
                    break;
                default:
                    return;
            }

            _measurementResults = new MeasurementResults(protocol);
            _measurementResults.ButtonController += (aState, aO) => PanelControllerDelegate(aState, aO);
            _measurementResults.TriggerReadMeasurementsAsync += (aList, aMeasurement, aCancellation) => MeasurementReader(aList, aMeasurement, aCancellation);
            _measurementResults.ActionInfo += (aSender, aMessage, aType) => ForwardMessageToActionInfo(aSender, aMessage, aType);
            _measurementResults.Dock = DockStyle.Fill;

            _activePanelController.Panels[INDEX_OF_MEASUREMENT_RESULTS] = _measurementResults;

            _activePanelController.MeasurementReader += (aList, aMeasurement, aCancellation) => MeasurementReader(aList, aMeasurement, aCancellation);
            _activePanelController.PanelControllerDelegate += (aState, aObject) => PanelControllerDelegate(aState, aObject);
            _activePanelController.ActionInfo += (aSender, aMessage, aType) => ForwardMessageToActionInfo(aSender, aMessage, aType);
        }

        /// <summary>
        /// Delegate-Object zum weiterleiten der Buttonfunktionen
        /// </summary>
        public EventHandler.PanelControlDelegate PanelControllerDelegate { get; set; }

        /// <summary>
        /// Delegate zum auslesen der Messwerte
        /// </summary>
        internal EventHandler.ReadMeasurementDelegate MeasurementReader { get; set; }

        /// <summary>
        /// Delegate zum schreiben von ActionInfo Meldungen
        /// </summary>
        internal EventHandler.ActionInfoEventHandler ForwardMessageToActionInfo { get; set; }

        /// <summary>
        /// Enthält die initialisierten Panels
        /// </summary>
        internal Control[] Panels => _activePanelController.Panels;

        /// <summary>
        /// Stoppt das Auslesen der Messwerte.
        /// </summary>
        internal void StopCommunication() => Context.RunInUiContext(() => _measurementResults.StopCommunication());

        /// <summary>
        /// Zeigt das initialisierte Haupt-Panel an
        /// </summary>
        internal void ShowMainPanel()
        {
            Context.RunInUiContext(() => _activePanelController.ShowMainPanel());
        }

        /// <summary>
        /// Setzt die Modulkonfiguration
        /// </summary>
        /// <param name="aModuleConfig">Modulkonfiguration</param>
        internal void SetModuleConfiguration(ModuleConfig aModuleConfig)
        {
            Context.RunInUiContext(() =>
                                   {
                                       _activePanelController.SetModuleConfiguration(aModuleConfig);
                                       _measurementResults.SetModuleConfiguration(aModuleConfig);
                                   });
        }

        /// <summary>
        /// Zeigt das UserControl mit den ausgelesenen Messwerten an.
        /// </summary>
        internal async Task ShowResultMeasurementsAsync(IEnumerable<MeasurementDataEntry> aMeasuredValueList)
        {
            Context.RunInUiContext(() =>
                                   {

                                       _measurementResults.UpdateDataSource(aMeasuredValueList);
                                       _measurementResults.Update();
                                       _measurementResults.BringToFront();
                                       _measurementResults.SetFocus();

                                       _addRibbonGroupsDelegate(_measurementResults.mainViewRibbonPage.Groups.ToArray());
                                   });

            await _measurementResults.ReadMeasurementsAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Aktualisiert die Daten in der TreeList
        /// </summary>
        /// <param name="aMeasurementList"></param>
        internal void UpdateDataSource(IList<IPrimitiveParameter> aMeasurementList) => Context.RunInUiContext(() => _activePanelController.UpdateAvailableMeasurements(aMeasurementList));

        /// <summary>
        /// Zeigt die Odx-Namen in der GUI an.
        /// </summary>
        /// <param name="aShow"></param>
        internal void ShowOdxNames(bool aShow)
        {
            Context.RunInUiContext(() =>
                                   {
                                       _activePanelController.ShowOdxNames(aShow);
                                       _measurementResults.ShowOdxNames(aShow);
                                   });
        }

        /// <summary>
        /// Zeigt die ServiceIdentifier in der GUI an.
        /// </summary>
        /// <param name="aShow"></param>
        internal void ShowServiceIdentifier(bool aShow)
        {
            Context.RunInUiContext(() =>
                                   {
                                       _activePanelController.ShowServiceIdentifier(aShow);
                                       _measurementResults.ShowServiceIdentifier(aShow);
                                   });
        }

        /// <summary>
        /// Setzt die Zykluszeit
        /// </summary>
        /// <param name="aCycleTime">Zykluszeit</param>
        internal void SetCycleTime(double aCycleTime) => _measurementResults.SetCycleTime(aCycleTime);
    }
}
