// /*
//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 21.02.2017
//  */

namespace MeasurementsModule.Panels
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Data;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using Events;

    /// <summary>
    /// Allgemeine Steuerungsklasse der UserControls
    /// </summary>
    internal abstract class AbstractPanelController
    {
        /// <summary>
        /// Initialisiert den Abstrakten PanelController
        /// </summary>
        internal AbstractPanelController()
        {
            Panels = new Control[2];
        }

        /// <summary>
        /// Delegate-Object zum weiterleiten der Buttonfunktionen
        /// </summary>
        internal EventHandler.PanelControlDelegate PanelControllerDelegate { get; set; }

        /// <summary>
        /// Delegate zum schreiben von ForwardMessageToActionInfo Meldungen
        /// </summary>
        internal EventHandler.ActionInfoEventHandler ActionInfo { get; set; }

        /// <summary>
        /// Enthält alle initialisierten Panels
        /// </summary>
        internal Control[] Panels { get; }

        /// <summary>
        /// Delegate zum auslesen der Messwerte
        /// </summary>
        internal EventHandler.ReadMeasurementDelegate MeasurementReader { get; set; }

        /// <summary>
        /// Zeigt das Haupt-Panel an.
        /// </summary>
        protected internal abstract void ShowMainPanel();

        /// <summary>
        /// Setzt die Modulkonfiguration
        /// </summary>
        /// <param name="aModuleConfig"></param>
        protected internal abstract void SetModuleConfiguration(ModuleConfig aModuleConfig);

        /// <summary>
        /// Methode zum aktualisieren der Daten in der TreeList
        /// </summary>
        /// <param name="aMeasurementList"></param>
        protected internal abstract void UpdateAvailableMeasurements(IList<IPrimitiveParameter> aMeasurementList);

        /// <summary>
        /// Zeigt die Odx-Namen in der GUI an.
        /// </summary>
        protected internal abstract void ShowOdxNames(bool aShow);

        /// <summary>
        /// Zeigt die Service-IDs in der GUI an.
        /// </summary>
        protected internal abstract void ShowServiceIdentifier(bool aShow);
    }
}
