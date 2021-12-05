namespace MeasurementsModule.Panels
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using CsvLogging;
    using CsvLogging.Helper;
    using DevExpress.XtraBars;

    /// <summary>
    /// Prtielle Klasse mit den CSV Logging Funktionen
    /// </summary>
    internal sealed partial class MeasurementResults
    {
        /// <summary>
        /// Name der Windows Explorer.exe
        /// </summary>
        private const string EXPLORER_EXE = "explorer.exe";

        /// <summary>
        /// Explorer Parameter der einem Dateipfad voran gestellt werden kann, damit das sich öffnende Fenster die Datei direkt markiert hat
        /// </summary>
        private const string EXPLORER_EXE_PARAMETER_TO_HIGHLIGHT_FILE = "/e,/select,";

        /// <summary>
        /// Die letzte CSV Datei die vom Benutzer ausgewählt wurde
        /// </summary>
        private FileInfo _lastCsvFilePath;

        /// <summary>
        /// Wird ausgelöst, wenn der Button für den CSV Logging Start gedrückt wird
        /// </summary>
        /// <param name="aSender">Wird nicht benötigt</param>
        /// <param name="aEventArgs">Wird nicht benötigt</param>
        private void StartCsvLoggingBtnClicked(object aSender, ItemClickEventArgs aEventArgs) => StartCsvLogging();

        /// <summary>
        /// Wird ausgelöst, wenn der Button für den CSV Logging Stop gedrückt wird
        /// </summary>
        /// <param name="aSender">Wird nicht benötigt</param>
        /// <param name="aEventArgs">Wird nicht benötigt</param>
        private void StopCsvLoggingBtnClicked(object aSender, ItemClickEventArgs aEventArgs) => StopCsvLogging();

        /// <summary>
        /// Wird ausgelöst, wenn der Button zum öffnen des Dateienspeicherorts gedrückt wird
        /// </summary>
        /// <param name="aSender">Wird nicht benötigt</param>
        /// <param name="aEventArgs">Wird nicht benötigt</param>
        private void OpenFileLocationClicked(object aSender, ItemClickEventArgs aEventArgs)
        {
            if (_lastCsvFilePath?.Exists == true)
            {
                Process.Start(EXPLORER_EXE, EXPLORER_EXE_PARAMETER_TO_HIGHLIGHT_FILE + _lastCsvFilePath);
            }
            else
            {
                Process.Start(EXPLORER_EXE, FilePicker.MEASUREMENTS_PATH);
            }
        }

        /// <summary>
        /// Startet das CSV Logging
        /// </summary>
        private async void StartCsvLogging()
        {
            var csvLoggingFile = FilePicker.GetLoggingFileFromUser();

            if (csvLoggingFile != null)
            {
                await CsvLogger.SetCsvLoggingFileAsync(csvLoggingFile).ConfigureAwait(true);

                if (CsvLogger.StartCsvLogging())
                {
                    _startCsvRecordBarButtonItem.Enabled = false;
                    _stopCsvRecordBarButtonItem.Enabled = true;
                    _lastCsvFilePath = csvLoggingFile;
                }
            }
        }

        /// <summary>
        /// Stoppt das CSV Logging
        /// </summary>
        private void StopCsvLogging()
        {
            CsvLogger.StopCsvLogging();
            _startCsvRecordBarButtonItem.Enabled = true;
            _stopCsvRecordBarButtonItem.Enabled = false;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Initialmessung beendet ist
        /// </summary>
        private void OnInitialMeasurementFinished()
        {
            _startCsvRecordBarButtonItem.Enabled = true;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das MeasurementResult Fenster verlassen wird
        /// </summary>
        private void OnLeaveView()
        {
            _startCsvRecordBarButtonItem.Enabled = false;
            _stopCsvRecordBarButtonItem.Enabled = false;
        }
    }
}
