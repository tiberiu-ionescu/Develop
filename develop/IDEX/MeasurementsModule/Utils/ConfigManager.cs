// // Copyright (c) AUDI AG 2017
// //
// // Autor: Funk, Jan (I/EE-87)
// // Erstellt: 30.03.2017

namespace MeasurementsModule.Utils
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using De.VwAudi.Common.Util.IO;
    using De.VwAudi.Idex.Common.Constants;
    using De.VwAudi.Idex.Common.Gui.Data;

    /// <summary>
    /// Klasse übernimmt das Laden und Speichern der Konfiguration
    /// </summary>
    internal static class ConfigManager
    {
        /// <summary>
        /// Dialog zum öffnen von Dateien.
        /// Wird verwendet um eine Konfiguration aus einer XML-Datei zu laden.
        /// </summary>
        private static readonly OpenFileDialog OPEN_FILE_DIALOG;

        /// <summary>
        /// Dialog zum speichern von Dateien.
        /// Wird verwendet um eien Konfiguration in einer XML-Datei abzuspeichern.
        /// </summary>
        private static readonly SaveFileDialog SAVE_FILE_DIALOG;

        /// <summary>
        /// Initialisiert die Member der Klasse
        /// </summary>
        static ConfigManager()
        {
            string appPath = Path.Combine(PathConstants.USER_CONFIGS_DIR, @"Measurements");

            SAVE_FILE_DIALOG = new SaveFileDialog
                               {
                                   InitialDirectory =
                                       FileOperations.CreateDirectory(appPath)
                                           ? appPath
                                           : PathConstants.USER_CONFIGS_DIR,
                                   Filter = @"XML Files | *.XML"
                               };

            OPEN_FILE_DIALOG = new OpenFileDialog
                               {
                                   InitialDirectory =
                                       FileOperations.CreateDirectory(appPath)
                                           ? appPath
                                           : PathConstants.USER_CONFIGS_DIR,
                                   Filter = @"XML Files | *.XML"
                               };
        }

        /// <summary>
        /// Speichert eine Konfiguration
        /// </summary>
        /// <param name="aMeasurementDataEntries">Liste mit Messwerten</param>
        /// <returns></returns>
        internal static bool SaveConfiguration(List<MeasurementDataEntry> aMeasurementDataEntries)
            => SAVE_FILE_DIALOG.ShowDialog() == DialogResult.OK && XmlCodec.SaveToXml(aMeasurementDataEntries, SAVE_FILE_DIALOG.FileName);

        /// <summary>
        /// Lädt eine Konfiguration
        /// </summary>
        /// <returns></returns>
        internal static List<MeasurementDataEntry> LoadConfiguration()
            =>
                OPEN_FILE_DIALOG.ShowDialog() != DialogResult.OK
                    ? null
                    : XmlCodec.ReadFromXml<List<MeasurementDataEntry>>(OPEN_FILE_DIALOG.FileName);
    }
}
