// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 19.03.2020

namespace MeasurementsModule.CsvLogging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using De.VwAudi.Idex.Common.Gui.Data;
    using NLog;
    using Types;

    /// <summary>
    /// Klasse zum verwalten der CSV Logging-Funktion von dem Messwerten
    /// </summary>
    internal static class CsvLogger
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// CSV Datei in die Daten geschrieben werden
        /// </summary>
        private static FileInfo _fileToWrite;

        /// <summary>
        /// Gibt an ob die Schreib Funktion aktiviert ist.
        /// </summary>
        private static bool _isLoggerEnabled;

        /// <summary>
        /// Gibt die CSV Datei an in die gespeichert werden soll
        /// </summary>
        /// <param name="aNewFile">Datei in welche die Messwerte gespeichert werden</param>
        public static async Task SetCsvLoggingFileAsync(FileInfo aNewFile)
        {
            if (await PrepareCsvLoggingFileAsync(aNewFile).ConfigureAwait(false))
            {
                _fileToWrite = aNewFile;
            }
            else
            {
                _fileToWrite = null;
                _isLoggerEnabled = false;
            }
        }

        /// <summary>
        /// Aktiviert den CsvLogger
        /// </summary>
        /// <returns><c>true</c> wenn das Starten erfolgreich war, sonst <c>false</c></returns>
        public static bool StartCsvLogging()
        {
            if (_fileToWrite == null)
            {
                _isLoggerEnabled = false;
            }
            else
            {
                _isLoggerEnabled = true;
                WriteCsvWorker.StartTimedWriting();
            }

            return _isLoggerEnabled;
        }

        /// <summary>
        /// Deaktiviert den CsvLogger
        /// </summary>
        public static void StopCsvLogging()
        {
            _isLoggerEnabled = false;
            WriteCsvWorker.StopTimedWriting();
        }

        /// <summary>
        /// Schreibt die übergebenen Daten in die CSV Datei
        /// </summary>
        /// <param name="aDataToWrite">Daten die geschrieben werden sollen</param>
        /// <returns>Task</returns>
        internal static async Task AddDataToWriteAsync(IList<MeasurementDataEntry> aDataToWrite)
        {
            if (_isLoggerEnabled)
            {
                var mappedData = await DataMapper.MapMeasurementDataToCsvListAsync(aDataToWrite).ConfigureAwait(false);
                await WriteCsvWorker.SendDataToWriteAsync(mappedData, _fileToWrite).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Bereitet die Logging Datei zum speichern der CSV Daten vor
        /// </summary>
        /// <param name="aNewFile">Datei die vorbereitet wird</param>
        /// <returns><c></c>true falls die Datei erfogreich vorbereitet wurde, sonst <c>false</c></returns>
        private static async Task<bool> PrepareCsvLoggingFileAsync(FileInfo aNewFile)
        {
            try
            {
                aNewFile.Refresh();

                if (aNewFile.Exists)
                {
                    if (IsEmptyFile(aNewFile))
                    {
                        await AddFileHeaderAsync(aNewFile).ConfigureAwait(false);
                    }
                }
                else
                {
                    aNewFile.Create().Close();
                    aNewFile.Refresh();

                    await AddFileHeaderAsync(aNewFile).ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception exception)
            {
                LOGGER.Error(exception, $"CSV Logger error while preparing CSV Logging File '{aNewFile?.FullName}");

                return false;
            }
        }

        /// <summary>
        /// Prüft ob die angegebene Datei leer ist
        /// </summary>
        /// <param name="aNewFile">Datei die vorbereitet wird</param>
        /// <returns><c>true</c> falls die Datei leer ist, sonst <c>false</c></returns>
        private static bool IsEmptyFile(FileInfo aNewFile)
        {
            return aNewFile.Length == 0;
        }

        /// <summary>
        /// Fügt den File Header der Worker Liste hinzu
        /// </summary>
        /// <param name="aNewFile">Datei die vorbereitet wird</param>
        private static Task AddFileHeaderAsync(FileInfo aNewFile)
        {
            var headerEntry = new List<CsvEntry>
                              {
                                  new CsvEntry
                                  {
                                      Timestamp = nameof(CsvEntry.Timestamp),
                                      ServiceId = nameof(CsvEntry.ServiceId),
                                      BlockName = nameof(CsvEntry.BlockName),
                                      BlockNameOdx = nameof(CsvEntry.BlockNameOdx),
                                      MeasurementName = nameof(CsvEntry.MeasurementName),
                                      MeasurementNameOdx = nameof(CsvEntry.MeasurementNameOdx),
                                      Value = nameof(CsvEntry.Value),
                                      Path = nameof(CsvEntry.Path),
                                  }
                              };

            return WriteCsvWorker.SendDataToWriteAsync(headerEntry, aNewFile);
        }
    }
}
