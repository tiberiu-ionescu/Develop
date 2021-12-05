// /*
//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 20.02.2017
//  */

namespace MeasurementsModule.Diagnose.UDS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.IdexBaseModule;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic.ServicesResult.Measurement;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Objects;
    using De.VwAudi.Idex.Common.Constants;
    using De.VwAudi.Idex.Common.Gui.Data;

    /// <summary>
    /// Klasse zum Auslesen der UDS-Messwerte
    /// </summary>
    internal sealed class UdsMeasurement : AbstractMeasurement
    {

        /// <summary>
        /// Liste mit Macrobefehlen, welche dem Script hinzugefügt wurden.
        /// </summary>
        private readonly List<string> _scriptLines;

        /// <summary>
        /// Initialisiert die Klasse UdsMeasurement
        /// </summary>
        /// <param name="aIdexApi">iDEX-Api Objekt, für den Zugriff auf den Diagnose-Layer</param>
        /// <param name="aLogicalLink">LogicalLink des Steuergerätes</param>
        /// <param name="aIDexScript">iDEXScript-Objekt zum Aufzeichnen der Macros</param>
        internal UdsMeasurement(IIdexApi aIdexApi, ILogicalLink aLogicalLink, Script aIDexScript)
            : base(aIdexApi, aLogicalLink, aIDexScript)
        {
            _scriptLines = new List<string>();
        }

        /// <summary>
        /// Liest die verfügbaren Messwerte aus dem Steuergerät aus.
        /// </summary>
        /// <returns></returns>
        protected internal override async Task<IList<IPrimitiveParameter>> GetAvailableMeasurementsAsync()
        {
            AvailableMeasurements =  await _idexApi.OdxData.GetAvailableMeasurementsAsync(_logicalLink).ConfigureAwait(false);
            return AvailableMeasurements;
        }

        /// <summary>
        /// Liest die übergebenen Messwerte asynchron aus.
        /// </summary>
        /// <param name="aMeasurementList">Liste mit Messwerten.</param>
        /// <param name="aProgress">Progress Objekt, welches den Status der ausgelesenen Messwerte zurück liefert.</param>
        /// <param name="aCancellation">Cancellation Token</param>
        /// <returns>Liste mit ausgelesenen Messwertblöcken.</returns>
        protected internal override async Task<IList<IReadMeasuredBlockResult>> ReadMeasuredValuesAsync(
            IList<MeasurementDataEntry> aMeasurementList, IProgress<IReadMeasuredBlockResult> aProgress, CancellationToken aCancellation)
        {
            RecordMacro(aMeasurementList);
            var res = await _idexApi.Diagnostics.Measurement.ReadMeasuredValueListAsync(_logicalLink,
                    aMeasurementList.Select(aX => aX.RawResponse).ToList(), aProgress, aCancellation).ConfigureAwait(false);
            return res;
        }


        /// <summary>
        /// Erzeugt das iDEX-Macro
        /// </summary>
        /// <param name="aMeasurementList">Liste mit Messwerten.</param>
        private void RecordMacro(IEnumerable<MeasurementDataEntry> aMeasurementList)
        {
            foreach (var shortName in aMeasurementList.Select(aX => aX.ShortName))
            {
                string scriptLine = string.Format(MacroCommands.READ_MEASUREMENT_VALUE_UDS, _ecuScriptVar, shortName);

                if (!_scriptLines.Contains(scriptLine))
                {
                    _iDexScript?.AddLine(scriptLine);
                    _scriptLines.Add(scriptLine);
                }
            }
        }
    }
}
