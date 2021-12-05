// /*
//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 21.02.2017
//  */

namespace MeasurementsModule.Events
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ActionInfoModule;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic.ServicesResult.Measurement;
    using De.VwAudi.Idex.Common.Gui.Data;
    using Enum;

    /// <summary>
    /// EventHandler
    /// </summary>
    internal static class EventHandler
    {
        /// <summary>
        /// Object sendet Informationen zum aktuellen Prozess.
        /// </summary>
        /// <param name="aSender">Sendendes Element</param>
        internal delegate void ActionInfoEventHandler(object aSender, string aMessage, ActionInfoType aActionInfoType);

        /// <summary>
        /// Delegate-Typ zum weiterleiten der Buttonfunktionen
        /// </summary>
        /// <param name="aState">Status</param>
        internal delegate Task PanelControlDelegate(PanelControlStates aState, object aO = null);

        /// <summary>
        /// Delegate-Typ zum auslesen der Messwerte
        /// </summary>
        /// <param name="aMeasurementList">Liste mit Messwerten, welche ausgelesen werden sollen.</param>
        /// <param name="aUpdatedMeasurement">Progress, welche feuert, wenn ein Messwert ausgelesen wurde.</param>
        /// <param name="aCancellation">Cancellation Token</param>
        internal delegate Task<IList<IReadMeasuredBlockResult>> ReadMeasurementDelegate(IList<MeasurementDataEntry> aMeasurementList, IProgress<IReadMeasuredBlockResult> aUpdatedMeasurement, CancellationToken aCancellation);
    }
}
