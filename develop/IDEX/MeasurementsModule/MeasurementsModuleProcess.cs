/*
 * Copyright (c) AUDI AG 2017
 *
 * Autor: Funk, Jan (I/EE-45)
 * Erstellt: 20.2.2017
 */

namespace MeasurementsModule
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.IdexBaseModule;
    using Core.Types;
    using CsvLogging.Helper;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic.ServicesResult.Measurement;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Enum;
    using De.VwAudi.Idex.Common.Gui.Data;
    using Diagnose;
    using Diagnose.KWP;
    using Diagnose.UDS;

    /// <summary>
    /// Prozessklasse, zum Zugriff auf den Diagnose-Layer
    /// </summary>
    public class MeasurementsModuleProcess : BaseModuleProcess
    {
        /// <summary>
        /// Klasse zum auslesen der Messwerte
        /// </summary>
        private AbstractMeasurement _measurement;

        /// <summary>
        /// Aktives Diagnose-Protokoll
        /// </summary>
        internal Protocol ActiveProtocol => LogicalLink.Protocol;

        /// <summary>
        /// Objekt, welches den Zugriff auf die Datenbanken regelt
        /// </summary>
        internal DataBases DatabaseAccessor => DataBases;

        /// <summary>
        /// Initialisiert die Prozess-Klasse
        /// </summary>
        public override void Initialize() => Logger.Debug("Initialization done.");

        /// <summary>
        /// Initialisiert die richtige Messwertklasse anhand des Diagnoseprotokolles
        /// </summary>
        /// <returns></returns>
        internal async Task<bool> InitAsync() => await Task.Run(async () =>
        {
            bool isOk;
            FilePicker.ActiveLogicalLink = LogicalLink;

            switch (LogicalLink.Protocol)
            {
                case Protocol.UdsOnCan:
                    _measurement = new UdsMeasurement(IdexApi, LogicalLink, Script);
                    await ReadAvailableUdsMeasurementsAsync().ConfigureAwait(false);
                    Logger.Debug("initialize UDS measurements module");
                    isOk = true;
                    break;
                case Protocol.Kwp1281OnKline:
                case Protocol.Kwp1281OnTp16:
                case Protocol.Kwp2000OnTp20Can:
                case Protocol.Kwp2000OnTp16:
                case Protocol.Kwp2000OnKline:
                    _measurement = new KwpMeasurement(IdexApi, LogicalLink, Script);
                    Logger.Debug("initialize KWP measurements module");
                    isOk = true;
                    break;

                default:
                    Logger.Error($"Protocol {LogicalLink.Protocol} is not supported.");
                    isOk = false;
                    break;
            }

            return isOk;
        }).ConfigureAwait(false);


        /// <summary>
        /// Gibt die ausgelesenen Messwerte in der für die GUI anzeigbaren Sturktur zurück
        /// </summary>
        /// <returns></returns>
        private async Task ReadAvailableUdsMeasurementsAsync() => await _measurement.GetAvailableMeasurementsAsync().ConfigureAwait(false);

        /// <summary>
        /// Gibt die ausgelesenen Messwerte zurück
        /// </summary>
        /// <returns></returns>
        internal IList<IPrimitiveParameter> GetAvailableUdsMeasurements() => _measurement.AvailableMeasurements;

        /// <summary>
        /// Liest die übergebenen Messwerte asynchron aus.
        /// </summary>
        /// <param name="aMeasurementList">Liste auszulesender Messwerte.</param>
        /// <param name="aProgress">Progress</param>
        /// <param name="aCancellation">Cancellation-Token</param>
        /// <returns></returns>
        internal async Task<IList<IReadMeasuredBlockResult>> ReadMeasuredValuesAsync(IList<MeasurementDataEntry> aMeasurementList, IProgress<IReadMeasuredBlockResult> aProgress, CancellationToken aCancellation)
            => await _measurement.ReadMeasuredValuesAsync(aMeasurementList, aProgress, aCancellation).ConfigureAwait(false);

        /// <summary>
        /// Druckt das iDEX-Protokoll
        /// </summary>
        internal void PrintProtocol() => AcdcAccessor.PrintProtocol(AcdcAccessor.GetDefaultPrintRequirements());

        /// <summary>
        /// Speichert einen Messwertblock in der VehicleData
        /// </summary>
        /// <param name="aMeasuredValue"></param>
        internal void SaveMeasuredValueInVehicleData(IReadMeasuredBlockResult aMeasuredValue) => AcdcAccessor.AddMeasurements(aMeasuredValue);
    }
}
