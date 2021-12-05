// /*
//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 21.02.2017
//  */

namespace MeasurementsModule.Panels.KWP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Data;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using DevExpress.XtraBars.Ribbon;

    /// <summary>
    /// Klasse zum Steuern der KWP UserControls
    /// </summary>
    internal sealed class KwpPanelController : AbstractPanelController
    {
        /// <summary>
        /// Index dieses UserControls
        /// </summary>
        private const int INDEX_OF_KWP_MEASUREMENTS = 0;

        /// <summary>
        ///  UserControl, welche die verfügbaren KWP-Messwerte anzeigt
        /// </summary>
        private readonly KwpMeasurements _kwpMeasurements;

        /// <summary>
        /// Delegat zum hinzufügen von Ribbon-Pages-Gruppen zu dem Hauptribbon
        /// </summary>
        private readonly Action<RibbonPageGroup[]> _addRibbonGroupsDelegate;

        /// <summary>
        /// Intitialisiert den KWP-PanelController
        /// </summary>
        public KwpPanelController(Action<RibbonPageGroup[]> aAddRibbonGroupsDelegate)
        {
            _addRibbonGroupsDelegate = aAddRibbonGroupsDelegate;

            _kwpMeasurements = new KwpMeasurements();
            _kwpMeasurements.WriteToActionInfo += (aSender, aMessage, aType) => ActionInfo(aSender, aMessage, aType);
            _kwpMeasurements.ExecuteButtonFunctions += (aState, aO) => PanelControllerDelegate(aState, aO);
            _kwpMeasurements.Dock = DockStyle.Fill;

            Panels[INDEX_OF_KWP_MEASUREMENTS] = _kwpMeasurements;
        }

        protected internal override void SetModuleConfiguration(ModuleConfig aModuleConfig)
        {
            //In diesem Control gibt es keine Modul-Konfiguration die man setzen könnte
        }

        /// <summary>
        /// Zeigt das Hauptfenster zum auslesen der Messwerte an
        /// </summary>
        protected internal override void ShowMainPanel()
        {
            _kwpMeasurements.Update();
            _kwpMeasurements.BringToFront();
            _kwpMeasurements.Refresh();
            _kwpMeasurements.SetFocus();

            _addRibbonGroupsDelegate(_kwpMeasurements.mainViewRibbonPage.Groups.ToArray());
        }

        /// <summary>
        /// Zeigt die Odx-Namen in der GUI an.
        /// </summary>
        protected internal override void ShowOdxNames(bool aShow)
        {
            //Die Spalten sind in diesem Control nicht vorhanden
        }

        /// <summary>
        /// Zeigt die Service-IDs in der GUI an.
        /// </summary>
        protected internal override void ShowServiceIdentifier(bool aShow)
        {
            //Die Spalten sind in diesem Control nicht vorhanden
        }

        /// <summary>
        /// Aktualisiert die GUI
        /// </summary>
        /// <param name="aMeasurementList"></param>
        protected internal override void UpdateAvailableMeasurements(IList<IPrimitiveParameter> aMeasurementList)
        {
            //Methode darf nur bei UDS aufgerufen werden.
        }
    }
}
