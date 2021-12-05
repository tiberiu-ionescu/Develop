// /*
//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 21.02.2017
//  */

namespace MeasurementsModule.Panels.UDS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Data;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using DevExpress.XtraBars.Ribbon;

    /// <summary>
    /// Klasse zum Steuern der UDS UserControls
    /// </summary>
    internal sealed class UdsPanelController : AbstractPanelController
    {
        /// <summary>
        /// Index dieses UserControls
        /// </summary>
        private const int INDEX_OF_UDS_MEASUREMENTS = 0;

        /// <summary>
        /// UserControl, welche die verfügbaren Messwerte anzeigt
        /// </summary>
        private readonly UdsMeasurements _availableMeasurements;

        /// <summary>
        /// Delegat zum hinzufügen von Ribbon-Pages-Gruppen zu dem Hauptribbon
        /// </summary>
        private readonly Action<RibbonPageGroup[]> _addRibbonGroupsDelegate;

        /// <summary>
        /// Initialisiert den UdsPanelController
        /// </summary>
        public UdsPanelController(Action<RibbonPageGroup[]> aAddRibbonGroupsDelegate)
        {
            _addRibbonGroupsDelegate = aAddRibbonGroupsDelegate;

            _availableMeasurements = new UdsMeasurements();
            _availableMeasurements.ButtonController += (aState, aO) => PanelControllerDelegate(aState, aO);
            _availableMeasurements.ActionInfo += (aSender, aMessage, aType) => ActionInfo(aSender, aMessage, aType);
            _availableMeasurements.Dock = DockStyle.Fill;
            _availableMeasurements.ShowOdxNames(false);
            _availableMeasurements.ShowServiceIdentifier(false);

            Panels[INDEX_OF_UDS_MEASUREMENTS] = _availableMeasurements;
        }

        /// <summary>
        /// Setzt die Modulkonfiguration
        /// </summary>
        /// <param name="aModuleConfig">Modulkonfiguration</param>
        protected internal override void SetModuleConfiguration(ModuleConfig aModuleConfig)
        {
            ShowOdxNames(aModuleConfig.ShowOdxNames);
            ShowServiceIdentifier(aModuleConfig.ShowServiceIds);
        }

        /// <summary>
        /// Zeigt das Hauptfenster zum auslesen der Messwerte an
        /// </summary>
        protected internal override void ShowMainPanel()
        {
            _availableMeasurements.Update();
            _availableMeasurements.BringToFront();
            _availableMeasurements.Refresh();
            _availableMeasurements.SetFocus();

            _addRibbonGroupsDelegate(_availableMeasurements.mainViewRibbonPage.Groups.ToArray());
        }

        /// <summary>
        /// Zeigt die Odx-Namen in der GUI an.
        /// </summary>
        protected internal override void ShowOdxNames(bool aShow) => _availableMeasurements.ShowOdxNames(aShow);

        /// <summary>
        /// Zeigt die Service-IDs in der GUI an.
        /// </summary>
        protected internal override void ShowServiceIdentifier(bool aShow) => _availableMeasurements.ShowServiceIdentifier(aShow);

        /// <summary>
        /// Aktualisiert die Daten der GUI
        /// </summary>
        /// <param name="aMeasurementList"></param>
        protected internal override void UpdateAvailableMeasurements(IList<IPrimitiveParameter> aMeasurementList) => _availableMeasurements.UpdateDataSource(aMeasurementList);
    }
}
