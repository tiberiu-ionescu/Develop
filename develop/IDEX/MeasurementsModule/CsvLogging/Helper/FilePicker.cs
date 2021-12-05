// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 27.03.2020

namespace MeasurementsModule.CsvLogging.Helper
{
    using System;
    using System.IO;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Objects;
    using De.VwAudi.Idex.Common.Constants;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Mask;
    using NLog;
    using Strings;

    /// <summary>
    /// Klasse stellt Funktionen bereit um eine Logging CSV Datei auszuwählen
    /// </summary>
    internal static class FilePicker
    {
        /// <summary>
        /// Dateisuffix für CSV Dateien
        /// </summary>
        private const string CSV_FILE_EXTENSION = ".csv";

        /// <summary>
        /// Formatierugsstring für den Datum und Uhrzeit Wert
        /// </summary>
        private const string DATE_TIME_FORMAT = "yyyy.MM.dd_HH.mm.ss";

        /// <summary>
        /// Trennzeichen mit dem der vorgeschlagene Dateiname getrennt wird
        /// </summary>
        private const string FILENAME_DELIMITER = "_";

        /// <summary>
        /// Regex für den Dateinamen
        /// </summary>
        private const string FILENAME_REGEX = "(([^~\"#%&*:<>?\\/\\\\{|}])+(\\.csv))";

        /// <summary>
        /// Formatierung für die Baudadresse
        /// </summary>
        private const string BAUD_ADDRESS_FORMAT = "D4";

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Pfad zu dem Ordner in dem alle Messungen enthalten sind
        /// </summary>
        internal static readonly string MEASUREMENTS_PATH = Path.Combine(PathConstants.IDEX_PUBLIC_DOCUMENTS, "Measurements");

        /// <summary>
        /// Referenz auf den LogicalLink von dem ausgewählten Steuergerät
        /// </summary>
        internal static ILogicalLink ActiveLogicalLink { private get; set; }

        /// <summary>
        /// Fragt den Benutzer nach einem Dateipfad zu der CSV Datei
        /// </summary>
        /// <returns>Pfad zu CSV Datei wenn erfolgreich. 
        /// Beim fehlschlagen oder abbruch durch Benutzer<c>null</c></returns>
        public static FileInfo GetLoggingFileFromUser()
        {
            FileInfo csvLoggingFile = null;

            string fileSelectionResult = XtraInputBox.Show(CreateInputBoxArgs())?.ToString();

            if (!string.IsNullOrEmpty(fileSelectionResult))
            {
                try
                {
                    Directory.CreateDirectory(MEASUREMENTS_PATH);
                    csvLoggingFile = new FileInfo(Path.Combine(MEASUREMENTS_PATH, fileSelectionResult));
                    csvLoggingFile = CleanFileIfRequired(csvLoggingFile);
                }
                catch (Exception exception)
                {
                    LOGGER.Error(exception, "CSV Logger error while preparing user selected Logfile");
                }
            }

            return csvLoggingFile;
        }

        /// <summary>
        /// Erstellt die Argumente (Einstellungen) für die IputBox
        /// </summary>
        /// <returns>Erstelltes Argumente Objekt</returns>
        private static XtraInputBoxArgs CreateInputBoxArgs()
        {
            string suggestedFileName = CreateSuggestedFileName();

            var args = new XtraInputBoxArgs();
            args.Prompt = Resource.CSV_FILE_DIALOG_PROMPT;
            args.Caption = Resource.CSV_FILE_DIALOG_TITLE;
            args.DefaultResponse = suggestedFileName;

            var editor = new TextEdit();
            editor.Properties.Mask.EditMask = FILENAME_REGEX;
            editor.Properties.Mask.MaskType = MaskType.RegEx;
            args.Editor = editor;
            return args;
        }

        /// <summary>
        /// Erstellt einen Dateinamen der dem Benutzer vorgeschlagen wird
        /// </summary>
        /// <returns>Vorgeschlagene Dateiname</returns>
        private static string CreateSuggestedFileName()
        {
            string projectName = ActiveLogicalLink?.Project?.ShortName ?? string.Empty;
            string baudAdress = ActiveLogicalLink?.LogicalLinkEntity?.BaudAddress.ToString(BAUD_ADDRESS_FORMAT) ?? string.Empty;
            string timeStamp = DateTime.Now.ToString(DATE_TIME_FORMAT);

            return projectName + FILENAME_DELIMITER + baudAdress + FILENAME_DELIMITER + timeStamp + CSV_FILE_EXTENSION;
        }

        /// <summary>
        /// Fragt den Benutzer ob der Inhalt der CSV Datei gelöscht werden soll und löscht diesen wenn erwünscht
        /// </summary>
        /// <param name="aCsvLoggingFile">Pfad zu der CSV Datei</param>
        private static FileInfo CleanFileIfRequired(FileInfo aCsvLoggingFile)
        {
            if (IsCleaningRequired(aCsvLoggingFile))
            {
                var userResponse = MessageBoxCreator.DataOverwriteQuestion();

                if (userResponse == MessageBoxCreator.UserDecision.OverwriteData)
                {
                    CleanFile(aCsvLoggingFile);
                }
                else if (userResponse == MessageBoxCreator.UserDecision.Abort)
                {
                    aCsvLoggingFile = null;
                }
            }

            return aCsvLoggingFile;
        }

        /// <summary>
        /// Gibt an ob der Inhalt einer Datei gelöscht werden kann.
        /// Das ist der Fall wenn die Datei vorhandne ist und Daten enthält
        /// </summary>
        /// <param name="aCsvLoggingFile">Pfad zu der CSV Datei</param>
        /// <returns><c>true</c> wenn der Inhalt gelöscht werden kann, sonst <c>false</c></returns>
        private static bool IsCleaningRequired(FileInfo aCsvLoggingFile)
        {
            return aCsvLoggingFile.Exists && aCsvLoggingFile.Length > 0;
        }

        /// <summary>
        /// Löscht den Inhalt der CSV Datei
        /// </summary>
        /// <param name="aCsvLoggingFile">Pfad zu der CSV Datei</param>
        private static void CleanFile(FileInfo aCsvLoggingFile)
        {
            File.WriteAllText(aCsvLoggingFile.FullName, string.Empty);
            aCsvLoggingFile.Refresh();
        }
    }
}
