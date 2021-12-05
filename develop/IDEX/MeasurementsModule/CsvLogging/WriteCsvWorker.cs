// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 19.03.2020

namespace MeasurementsModule.CsvLogging
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using NLog;
    using Types;
    using Timer = System.Timers.Timer;

    /// <summary>
    /// Der Background Worker der die Einträge in die CSV Datei schreibt
    /// </summary>
    internal static class WriteCsvWorker
    {
        /// <summary>
        /// Schwellwert für Datensätze (Einzelne Messwerte) ab denen die Datensätze in die CSV Datei geschreiben werden
        /// </summary>
        private const int MEASUREMENTS_WRITE_THRESHOLD = 100;

        /// <summary>
        /// Zyklus in ms in dem die Daten geschrieben werden, falls der Datensatz Schwellwert in dieser Zeit nicht erreicht wurde
        /// </summary>
        private const int AUTO_WRITE_CYCLE_TIME_IN_MS = 10000;

        /// <summary>
        /// Maximale anzahl der parallel laufenden Worker Threads. Muss immer 1 sein.
        /// </summary>
        private const int MAX_THREADS = 1;

        /// <summary>
        /// Timer zum automatischen starten des Workers
        /// </summary>
        private static readonly Timer AUTO_WRITE_TIMER = new Timer(AUTO_WRITE_CYCLE_TIME_IN_MS);

        /// <summary>
        /// Semaphore zum sperren der gemeinsam genutzten Liste
        /// </summary>
        private static readonly SemaphoreSlim SEMAPHORE = new SemaphoreSlim(MAX_THREADS, MAX_THREADS);

        /// <summary>
        /// Liste mit CSV Einträgen die an den Worker übergeben wurden
        /// </summary>
        private static readonly IDictionary<FileInfo, List<CsvEntry>> WORKER_WRITE_TASKS = new Dictionary<FileInfo, List<CsvEntry>>();

        /// <summary>
        /// BackgrounWorker zum parallelen speichern der CSV Einträgen
        /// </summary>
        private static readonly BackgroundWorker WORKER = new BackgroundWorker();

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Konstruktor
        /// </summary>
        static WriteCsvWorker()
        {
            WORKER.WorkerReportsProgress = false;
            WORKER.WorkerSupportsCancellation = false;

            WORKER.DoWork += DoWork;

            AUTO_WRITE_TIMER.AutoReset = true;
            AUTO_WRITE_TIMER.Elapsed += OnAutoWriteTimerElapsed;
        }

        /// <summary>
        /// Klasse zum hinzufügen der Datensätze zum Worker.
        /// Der Zeitpunkt zu dem die Datensätze geschrieben werden entscheidet der Worker.
        /// </summary>
        /// <param name="aDataToWrite">Daten die geschrieben werden sollen</param>
        /// <param name="aFileToWrite">Datei in die Datensätze geschrieben werden sollen</param>
        /// <returns>Task</returns>
        internal static async Task SendDataToWriteAsync(List<CsvEntry> aDataToWrite, FileInfo aFileToWrite)
        {
            await SEMAPHORE.WaitAsync().ConfigureAwait(false);

            if (WORKER_WRITE_TASKS.Keys.Contains(aFileToWrite))
            {
                WORKER_WRITE_TASKS[aFileToWrite].AddRange(aDataToWrite);
            }
            else
            {
                WORKER_WRITE_TASKS.Add(aFileToWrite, aDataToWrite);
            }

            if (IsEnoughDataToWrite())
            {
                RunWorkerIfNotBusy();
            }

            SEMAPHORE.Release();
        }

        /// <summary>
        /// Schreibt die Datensätze die noch in der Warteliste sind sofort
        /// und hält das ContinuosWriting an
        /// </summary>
        internal static void StopTimedWriting()
        {
            AUTO_WRITE_TIMER.Stop();
            RunWorkerIfNotBusy();
        }

        /// <summary>
        /// Starten den Timer füs ContinuousWriting
        /// </summary>
        internal static void StartTimedWriting()
        {
            AUTO_WRITE_TIMER.Start();
        }

        /// <summary>
        /// Event wird ausgelöst, wenn der ContinuousWriting Timer abläuft
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aElapsedEventArgs">Nicht benötigt</param>
        private static void OnAutoWriteTimerElapsed(object aSender, ElapsedEventArgs aElapsedEventArgs) => RunWorkerIfNotBusy();

        /// <summary>
        /// Starten denn Worker, falls dieser nicht bereits läuft.
        /// </summary>
        private static void RunWorkerIfNotBusy()
        {
            if (!WORKER.IsBusy)
            {
                WORKER.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Prüft ob der Schwellwert für das Schreiben der Daten erreicht wurde.
        /// </summary>
        /// <returns><c>true</c> wenn die Daten geschrieben werden sollen, sonst <c>false</c></returns>
        private static bool IsEnoughDataToWrite()
        {
            int allValuesCount = WORKER_WRITE_TASKS.Values.Sum(aDataList => aDataList.Count);
            return allValuesCount > MEASUREMENTS_WRITE_THRESHOLD;
        }

        /// <summary>
        /// Wird ausgelöst wenn der Worker gestartet wird.
        /// Methode zum abarbeiten der Worker aufgabe
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aEventArgs">Nicht benötigt</param>
        private static void DoWork(object aSender, DoWorkEventArgs aEventArgs)
        {
            var timerWasEnabled = AUTO_WRITE_TIMER.Enabled;
            AUTO_WRITE_TIMER.Stop();

            try
            {
                while (WORKER_WRITE_TASKS.Any())
                {
                    var writeTask = ExtractMeasurementsToSave();
                    WriteCsvEntriesToFile(writeTask);
                }
            }
            catch (Exception exception)
            {
                LOGGER.Error(exception, "Error while writing CSV Logging Data");
            }

            if (timerWasEnabled)
            {
                AUTO_WRITE_TIMER.Start();
            }
        }

        /// <summary>
        /// Entnimmt der Werteliste die Daten die als nächstes geschrieben werden sollen.
        /// </summary>
        /// <returns>Liste mit den Daten die als nächstes geschrieben werden sollen</returns>
        private static KeyValuePair<FileInfo, List<CsvEntry>> ExtractMeasurementsToSave()
        {
            SEMAPHORE.Wait();

            var writeTask = WORKER_WRITE_TASKS.First();
            WORKER_WRITE_TASKS.Remove(writeTask);

            SEMAPHORE.Release();
            return writeTask;
        }

        /// <summary>
        /// Schreibt die CSV Einträge in die passende CSV Datei
        /// </summary>
        /// <param name="aWriteTask">Daten die geschrieben werden sollen und die Datei in die es geschrieben wird</param>
        private static void WriteCsvEntriesToFile(KeyValuePair<FileInfo, List<CsvEntry>> aWriteTask)
        {
            File.AppendAllLines(aWriteTask.Key.FullName, aWriteTask.Value.Select(aEntry => aEntry.ToString()), Encoding.UTF8);
        }
    }
}
