// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 19.03.2020

namespace MeasurementsModule.CsvLogging.Types
{
    using System.Text;

    /// <summary>
    /// Klassen Beschreibung
    /// </summary>
    internal sealed class CsvEntry
    {
        /// <summary>
        /// Trennzeichen für die CSV Werte
        /// </summary>
        private const string CSV_DELIMITER = "\t";

        /// <summary>
        /// Name des Messblocks
        /// </summary>
        internal string BlockName { private get; set; }

        /// <summary>
        /// ODX Name des Messblocks
        /// </summary>
        internal string BlockNameOdx { private get; set; }

        /// <summary>
        /// Name des Messwertes
        /// </summary>
        internal string MeasurementName { private get; set; }

        /// <summary>
        /// ODX Name des Messwertes
        /// </summary>
        internal string MeasurementNameOdx { private get; set; }

        /// <summary>
        /// Service ID
        /// </summary>
        internal string ServiceId { private get; set; }

        /// <summary>
        /// Zeitstempel wann die Werte erfasst wurden
        /// </summary>
        internal string Timestamp { private get; set; }

        /// <summary>
        /// Messwert
        /// </summary>
        internal string Value { private get; set; }

        /// <summary>
        /// Messwert
        /// </summary>
        internal string Path { private get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(Timestamp);
            builder.Append(CSV_DELIMITER);
            builder.Append(ServiceId);
            builder.Append(CSV_DELIMITER);
            builder.Append(BlockName);
            builder.Append(CSV_DELIMITER);
            builder.Append(BlockNameOdx);
            builder.Append(CSV_DELIMITER);
            builder.Append(MeasurementName);
            builder.Append(CSV_DELIMITER);
            builder.Append(MeasurementNameOdx);
            builder.Append(CSV_DELIMITER);
            builder.Append(Value);
            builder.Append(CSV_DELIMITER);
            builder.Append(Path);

            return builder.ToString();
        }
    }
}
