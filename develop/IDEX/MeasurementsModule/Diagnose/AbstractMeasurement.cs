// /*
//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 20.02.2017
//  */

namespace MeasurementsModule.Diagnose
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.IdexBaseModule;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic.ServicesResult.Measurement;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Objects;
    using De.VwAudi.Idex.Common.Gui.Data;

    /// <summary>
    /// Klassse zum auslesen der Messwerte
    /// </summary>
    internal abstract class AbstractMeasurement
    {
        /// <summary>
        /// Verfügbare UDS-Messwerte
        /// </summary>
        private IList<IPrimitiveParameter> _availableMeasurements = new List<IPrimitiveParameter>();

        /// <summary>
        /// iDEX-Api für den Zugriff auf Diagnosefunktionen
        /// </summary>
        protected readonly IIdexApi _idexApi;

        /// <summary>
        /// iDEX Script Object, zum Aufzeichnen von Macros
        /// </summary>
        protected readonly Script _iDexScript;

        /// <summary>
        /// Enthält das LogicalLink Object zum Steuergerät
        /// </summary>
        protected readonly ILogicalLink _logicalLink;

        /// <summary>
        /// Ecu-Variable für die Macroaufzeichnung
        /// </summary>
        protected readonly string _ecuScriptVar = string.Empty;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="aIdexApi">iDEX-Api für den Zugriff auf Diagnosefunktionen</param>
        /// <param name="aLogicalLink">Enthält das LogicalLink Object zum Steuergerät</param>
        /// <param name="aIDexScript">iDEX Script Object, zum aufzeichnen von Macros</param>
        protected AbstractMeasurement(IIdexApi aIdexApi, ILogicalLink aLogicalLink, Script aIDexScript)
        {
            _idexApi = aIdexApi;
            _logicalLink = aLogicalLink;
            _iDexScript = aIDexScript;

            _ecuScriptVar = $"ecu_0x{aLogicalLink.LogicalLinkEntity.BaudAddress.ToString("X")}";
        }

        /// <summary>
        /// Verfügbare UDS-Messwerte
        /// </summary>
        internal IList<IPrimitiveParameter> AvailableMeasurements
        {
            get { return _availableMeasurements ?? (_availableMeasurements = GetAvailableMeasurementsAsync().GetAwaiter().GetResult()); }
            set { _availableMeasurements = value; }
        }

        /// <summary>
        /// Liest die verfügbaren Messwerte aus dem Steuergerät aus.
        /// </summary>
        /// <returns></returns>
        protected internal abstract Task<IList<IPrimitiveParameter>> GetAvailableMeasurementsAsync();

        /// <summary>
        /// Liest die übergebenen Messwerte asynchron aus.
        /// </summary>
        /// <param name="aMeasurementList">Liste mit Messwerten.</param>
        /// <param name="aProgress">Progress Objekt, welches den Status der ausgelesenen Messwerte zurück liefert.</param>
        /// <param name="aCancellation">Cancellation Token</param>
        /// <returns>Liste mit ausgelesenen Messwertblöcken.</returns>
        protected internal abstract Task<IList<IReadMeasuredBlockResult>> ReadMeasuredValuesAsync(IList<MeasurementDataEntry> aMeasurementList, IProgress<IReadMeasuredBlockResult> aProgress, CancellationToken aCancellation);
    }
}
