// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 19.03.2020

namespace MeasurementsModule.CsvLogging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using De.VwAudi.Idex.Common.Gui.Data;
    using Types;
    using AbstractCommunicationDataContainer;

    /// <summary>
    /// Klasse zum Mappen der Measurement Daten auf CsvEntry
    /// </summary>
    internal static class DataMapper
    {
        /// <summary>
        /// Formatierugsstring für den Datum und Uhrzeit Wert
        /// </summary>
        private const string DATE_TIME_FORMAT = "yyyy.MM.dd_HH.mm.ss.fff";

        /// <summary>
        /// Mappt die <see cref="MeasurementDataEntry"/> Objekte auf <see cref="CsvEntry"/> Objekte
        /// </summary>
        /// <param name="aMeasurementDataEntries">Von hier werden die Daten ausgelesen</param>
        /// <returns>Listem mit <see cref="CsvEntry"/> mit den Messdaten</returns>
        public static async Task<List<CsvEntry>> MapMeasurementDataToCsvListAsync(IList<MeasurementDataEntry> aMeasurementDataEntries)
        {
            return await Task.Run(() => MapCycleToCsvEntries(aMeasurementDataEntries)).ConfigureAwait(false);
        }

        /// <summary>
        /// Mappt die Daten von einem Messzyklus
        /// </summary>
        /// <param name="aMeasurementDataEntries">Quelle für die Daten</param>
        /// <returns>Liste mit den gemappten Daten</returns>
        private static List<CsvEntry> MapCycleToCsvEntries(IList<MeasurementDataEntry> aMeasurementDataEntries)
        {
            string timeStamp = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var cycleCsvEntries = new List<CsvEntry>();

            foreach (var measurementDataEntry in aMeasurementDataEntries)
            {
                cycleCsvEntries.AddRange(MapBlockToCsvEntries(measurementDataEntry, timeStamp));
            }

            return cycleCsvEntries;
        }

        /// <summary>
        /// Ermittelt die Kindobjekte auf der niedrigsten Ebene, welche die Blattknoten des Teilbaums unter dem mitgegebenen Element repräsentieren, sowie den dazu entsprechenden Pfad
        /// </summary>
        /// <param name="aEntry">View Model Objekt, von dem die untergeordneten Blattknoten ermittelt werden sollen</param>
        /// <param name="aCurrentPath">Derzeitiger Pfad zum Teilbaum. Wird an die relativen Pfade vom Entry zu den Blattknoten hinzugefügt</param>
        /// <returns>Dictionary der untergeordneten Blattknoten und dem dazugehörigen Pfad. Wenn das Entry selbst ein Blattknoten ist, enthält das Dict lediglich das Entry selbst und den dazugehörigen Pfad</returns>
        private static IDictionary<MeasurementDataEntry,string> LowestDataEntryChildren(MeasurementDataEntry aEntry,string aCurrentPath)
        {
            var children = new Dictionary<MeasurementDataEntry,string>();
            if (aEntry.Children == null || aEntry.Children.Count == 0)
            {
                children.Add(aEntry,aCurrentPath);
                return children;
            }

            string amendedPath = aCurrentPath;
            if (!string.IsNullOrEmpty(amendedPath))
            {
                amendedPath += AcdcProvider.MEASUREMENT_PATH_DELIMITER;
            }

            amendedPath += aEntry.DisplayName;

            foreach (var child in aEntry.Children)
            {
                foreach (var kvp in LowestDataEntryChildren(child,amendedPath))
                {
                    children.Add(kvp.Key,kvp.Value);
                }
            }

            return children;
        }

        /// <summary>
        /// Mappt einen Block von Daten
        /// </summary>
        /// <param name="aMeasurementDataEntry">Quelle für die Daten</param>
        /// <param name="aTimeStamp">Zeitstempel wann die Einträge erzeugt wurden</param>
        /// <returns>Liste mit den gemappten Daten</returns>
        private static IEnumerable<CsvEntry> MapBlockToCsvEntries(MeasurementDataEntry aMeasurementDataEntry, string aTimeStamp)
        {
            var lowestChildrenDict = LowestDataEntryChildren(aMeasurementDataEntry,string.Empty);
            return lowestChildrenDict.Keys.Select(aEntry => MapMeasurementDataEntry(aMeasurementDataEntry, aEntry, aTimeStamp,lowestChildrenDict[aEntry]));
        }

        /// <summary>
        /// Mappt einen Eintrag
        /// </summary>
        /// <param name="aBlockMeasurementEntry">Der Parent block</param>
        /// <param name="aMeasurementDataEntry">Block Inhalt</param>
        /// <param name="aTimeStamp">Zeitstempel wann die Einträge erzeugt wurden</param>
        /// <param name="aPath">Pfad zum Entry</param>
        /// <returns>Ein gemappter Eintrag</returns>
        private static CsvEntry MapMeasurementDataEntry(MeasurementDataEntry aBlockMeasurementEntry, MeasurementDataEntry aMeasurementDataEntry, string aTimeStamp,string aPath)
        {
            return new CsvEntry
                   {
                       Timestamp = aTimeStamp,
                       ServiceId = aBlockMeasurementEntry.Identifier,
                       BlockName = aBlockMeasurementEntry.DisplayName,
                       BlockNameOdx = aBlockMeasurementEntry.ShortName,
                       MeasurementName = aMeasurementDataEntry.DisplayName,
                       MeasurementNameOdx = aMeasurementDataEntry.ShortName,
                       Value = aMeasurementDataEntry.Value,
                       Path = aPath
                   };
        }
    }
}
