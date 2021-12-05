// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 25.03.2020

namespace MeasurementsModule.CsvLogging.Helper
{
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using Strings;

    /// <summary>
    /// Klasse zu erstellen von MessageBoxen
    /// </summary>
    internal static class MessageBoxCreator
    {
        /// <summary>
        /// Button der bereits markiert ist beim DataOverwriteQuestion Dialog
        /// </summary>
        private const int DATA_OVERWRITE_QUESTION_DEFAULT_BUTTON_INDEX = 2;

        /// <summary>
        /// Fragt den Benutzer ob die vorhandene Datei überschrieben oder die Daten angehängt werden sollen.
        /// </summary>
        /// <returns><c>yes</c> wenn die Daten überschrieben werden sollen. 
        /// <c>No</c> wenn die Daten angehängt werden sollen. 
        /// <c>Abort</c> wenn die Auswahl abgebrochen wird</returns>
        public static UserDecision DataOverwriteQuestion()
        {
            var dialogResult = XtraMessageBox.Show(CreateMessageBoxArgs());

            return MapResultToEnum(dialogResult);
        }

        /// <summary>
        /// Erstellt die Konfiguration für die MessageBox
        /// </summary>
        /// <returns>Konfiguration für die MessageBox</returns>
        private static XtraMessageBoxArgs CreateMessageBoxArgs()
        {
            var args = new XtraMessageBoxArgs
                       {
                           Caption = Resource.CSV_OVERWRITE_MESSAGEBOX_CAPTION,
                           Text = Resource.CSV_OVERWRITE_MESSAGEBOX_TEXT,
                           Icon = SystemIcons.Information,
                           Buttons = new[] { DialogResult.Yes, DialogResult.No, DialogResult.Abort },
                           DefaultButtonIndex = DATA_OVERWRITE_QUESTION_DEFAULT_BUTTON_INDEX
                       };

            args.Showing += ArgsShowing;

            return args;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die MessageBox geladen wird. Hier werden die Buttons manuell angepasst.
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aMessageShowingArgs">Argumente zu der MessageBox</param>
        private static void ArgsShowing(object aSender, XtraMessageShowingArgs aMessageShowingArgs)
        {
            foreach (Control control in aMessageShowingArgs.Form.Controls)
            {
                var button = control as SimpleButton;
                if (button != null)
                {
                    switch (button.DialogResult)
                    {
                        case DialogResult.Yes:
                            button.Text = Resource.CSV_OVERWRITE_BUTTON_TEXT;
                            button.MinimumSize = button.Size;
                            button.AutoSize = true;
                            break;
                        case DialogResult.No:
                            button.Text = Resource.CSV_APPEND_BUTTON_TEXT;
                            button.MinimumSize = button.Size;
                            button.AutoSize = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Mappt das DialogResult auf das passende UserDecision Enum
        /// </summary>
        /// <param name="aDialogResult">DialogResult aus der MessageBox</param>
        /// <returns>Passenden Enum zum DialogResult</returns>
        private static UserDecision MapResultToEnum(DialogResult aDialogResult)
        {
            UserDecision userDecision;

            switch (aDialogResult)
            {
                case DialogResult.Yes:
                    userDecision = UserDecision.OverwriteData;
                    break;
                case DialogResult.No:
                    userDecision = UserDecision.AppendData;
                    break;
                default:
                    userDecision = UserDecision.Abort;
                    break;
            }

            return userDecision;
        }

        /// <summary>
        /// Enum für das vom Benutzer ausgewählte Verhalten falls die Datei bereits Inhalt hat
        /// </summary>
        internal enum UserDecision
        {
            /// <summary>
            /// Die vorhandene Daten sollen gelöscht werden
            /// </summary>
            OverwriteData,

            /// <summary>
            /// Die neuen Daten werden zu den bestehenden hinzugefügt
            /// </summary>
            AppendData,

            /// <summary>
            /// Der Auswahldialog wurde vom Benutzer abgebrochen
            /// </summary>
            Abort
        }
    }
}
