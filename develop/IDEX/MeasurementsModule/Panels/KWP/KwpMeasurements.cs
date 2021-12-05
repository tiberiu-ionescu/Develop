namespace MeasurementsModule.Panels.KWP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ActionInfoModule;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Enum;
    using De.VwAudi.Idex.Common.Gui.Helpers;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Mask;
    using Enum;
    using Strings;
    using Utils;
    using EventHandler = Events.EventHandler;

    internal sealed partial class KwpMeasurements : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Maximaler Wert eines Bytes
        /// </summary>
        private const byte BYTE_MAX_VALUE = 255;

        /// <summary>
        /// Konstante definiert den Character, mit welchem eine Sequenz von Bytes getrennt ist.
        /// </summary>
        private const char COMMA_SEPARATOR = ',';

        /// <summary>
        /// Konstante definiert den regulären Ausdruck für alle möglichen Werte eines Bytes.
        /// </summary>
        private const string BYTE_PATTERN = "(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])";

        /// <summary>
        /// String definiert den regulären Ausdruck für ein mögliches Pattern, in welchem Kanäle in einer Komma-getrennten Sequenz angegeben werden. Dieses Pattern ist z.B. <c>14,17,29,122</c>.
        /// </summary>
        private static readonly string CHANNEL_SEQUENCE_PATTERN = $"({BYTE_PATTERN}\\s*(,\\s*{BYTE_PATTERN})*)";

        /// <summary>
        /// String definiert den regulären Ausdruck für ein mögliches Pattern, in welchem Kanäle als Range angegeben werden. Dieses Pattern ist z.B. <c>17-234</c>.
        /// </summary>
        private static readonly string CHANNEL_RANGE_PATTERN = $"({BYTE_PATTERN}-{BYTE_PATTERN})";

        /// <summary>
        /// Initialisisert die Klasse zum Auslesen der KWP-Messwerte
        /// </summary>
        public KwpMeasurements()
        {
            InitializeComponent();

            //Diese Eigenschaft MUSS auf true gesetzt werden, da sonst keine Daten im GridView angezeigt werden.
            layoutControl1.UseLocalBindingContext = true;

            _channelNrEdit.Mask.MaskType = MaskType.RegEx;
            _channelNrEdit.Mask.EditMask = $@"{CHANNEL_RANGE_PATTERN}|{CHANNEL_SEQUENCE_PATTERN}";

            _measurementTreeList.InitSettingsByProtocol(Protocol.Kwp1281OnKline);

            SetButtonState();
        }

        /// <summary>
        /// Setzt den Focus in das Eingabefeld für die Kanalnummer
        /// </summary>
        internal void SetFocus() => _channelNrBarEditItem.Links.First().Focus();

        /// <summary>
        /// Delegate zum schreiben von ForwardMessageToActionInfo Meldungen
        /// </summary>
        internal EventHandler.ActionInfoEventHandler WriteToActionInfo { get; set; }

        /// <summary>
        /// Delegate-Object zum weiterleiten der Buttonfunktionen
        /// </summary>
        internal EventHandler.PanelControlDelegate ExecuteButtonFunctions { get; set; }

        /// <summary>
        /// Fügt einen/mehrere Kanäle der Liste hinzu
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.></param>
        private void BtnAddChannelClick(object aSender, ItemClickEventArgs aE) => AddChannels();

        /// <summary>
        /// Fügt die Kanäle aus dem Textfeld der TreeList hinzu
        /// </summary>
        private void AddChannels()
        {
            _measurementTreeList.UpdateMeasurementTreeList(GetChannelList(_channelNrBarEditItem.EditValue as string));
            _channelNrBarEditItem.EditValue = string.Empty;
            SetButtonState();
        }

        /// <summary>
        /// Erzeugt aus dem übergebenen Text eine Liste mit KWP-Kanälen
        /// </summary>
        /// <param name="aText"></param>
        /// <returns></returns>
        private static IEnumerable<byte> GetChannelList(string aText)
        {
            var channelList = new List<byte>();

            foreach (Match match in Regex.Matches(aText, CHANNEL_RANGE_PATTERN, RegexOptions.IgnoreCase))
            {
                if (!string.IsNullOrEmpty(match.Groups[1].Value))
                {
                    channelList.AddRange(ConvertByteRange(aText));
                }
            }

            foreach (Match match in Regex.Matches(aText, CHANNEL_SEQUENCE_PATTERN, RegexOptions.IgnoreCase))
            {
                if (!string.IsNullOrEmpty(match.Groups[1].Value))
                {
                    channelList.AddRange(ConvertByteSequence(match.Groups[1].Value));
                }
            }

            return channelList;
        }

        /// <summary>
        /// Methode konvertiert einen String einer Sequenz einzelner Bytes, welche durch Kommata getrennt sind, in eine Liste mit den jeweiligen Bytes. Zahlen, die größer als 255 sind, werden als 255 gespeichert.
        /// </summary>
        /// <param name="aText">String, der die Byte-Sequenz darstellt, z.B. <c>17,22,34,55</c></param>
        /// <returns></returns>
        private static IEnumerable<byte> ConvertByteSequence(string aText)
        {
            var channelList = new List<byte>();

            if (aText.Contains(COMMA_SEPARATOR))
            {
                channelList.AddRange(aText.Split(new[] { COMMA_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).Select(ConvertToByteSafely));
            }
            else
            {
                channelList.Add(ConvertToByteSafely(aText));
            }

            return channelList;
        }

        /// <summary>
        /// Methode konvertiert eine Range von Bytes in eine Liste, die aus den einzelnen Bytes besteht.
        /// </summary>
        /// <example>Der String <c>1-9</c> wird in eine Liste bestehend aus den Bytes <c>1, 2, 3, 4, 5, 6, 7, 8, 9</c> konvertiert.</example>
        /// <param name="aText">String, der eine Byte-Range darstellt, z.B. <c>17-34</c>.</param>
        /// <returns>Liste mit einzelnen Bytes, die aus <paramref name="aText"/> extrahiert wurden.</returns>
        private static IEnumerable<byte> ConvertByteRange(string aText)
        {
            var range = aText.Split('-');
            byte startPos = ConvertToByteSafely(range[0]);
            byte endPos = ConvertToByteSafely(range[1]);
            if (endPos <= startPos)
            {
                endPos = startPos;
            }

            return Enumerable.Range(startPos, endPos - startPos + 1).Select(aValue => (byte) aValue);
        }

        /// <summary>
        /// Methode überprüft, dass der Wert des Strings <paramref name="aValue"/> nicht größer als der Maximalwert eines Bytes ist und gibt den Wert des Strings als Byte zurück. 
        /// </summary>
        /// <param name="aValue">String, der ein Byte darstellt.</param>
        /// <returns></returns>
        private static byte ConvertToByteSafely(string aValue) => Convert.ToByte(Convert.ToInt32(aValue) > BYTE_MAX_VALUE ? BYTE_MAX_VALUE : Convert.ToInt32(aValue));

        /// <summary>
        /// Schält zum auslesen der Messwerte weiter
        /// </summary>
        /// <param name="aSender">Parameter wird nicht benutzt.</param>
        /// <param name="aE">Parameter wird nicht benutzt.</param>
        private void SBtnNextClick(object aSender, ItemClickEventArgs aE) => SwitchNext();

        /// <summary>
        /// Button zum Zurück Schalten
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void SBtnSwitchBackClick(object aSender, ItemClickEventArgs aE) => SwitchBack();

        /// <summary>
        /// Lädt eine Konfiguration
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void LoadConfigurationClick(object aSender, ItemClickEventArgs aE)
        {
            var measuredValuesToSelect = ConfigManager.LoadConfiguration();

            if (measuredValuesToSelect == null)
            {
                WriteToActionInfo(aSender, Resource.ConfigurationNotLoadable, ActionInfoType.Error);
            }
            else
            {
                _measurementTreeList.UpdateMeasurementTreeList(measuredValuesToSelect.Select(aDataEntry => aDataEntry.ChannelNumber));
                SetButtonState();
                WriteToActionInfo(aSender, Resource.ConfigurationLoadedSuccessfully, ActionInfoType.Info);
            }
        }

        /// <summary>
        /// Speichert eine Konfiguration
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void SaveConfigurationClick(object aSender, ItemClickEventArgs aE)
        {
            if (ConfigManager.SaveConfiguration(_measurementTreeList.SelectedMeasurements))
            {
                WriteToActionInfo(aSender, Resource.ConfigurationSavedSuccessfully, ActionInfoType.Info);
            }
            else
            {
                WriteToActionInfo(aSender, Resource.ConfigurationNotSaveable, ActionInfoType.Error);
            }
        }

        /// <summary>
        /// Löscht die aktuelle Konfiguration
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void RemoveConfigurationClick(object aSender, ItemClickEventArgs aE)
        {
            _measurementTreeList.ClearTreeList();
            _measurementTreeList.RefreshDataSource();
            WriteToActionInfo(aSender, Resource.ConfigurationRemoved, ActionInfoType.Info);
            SetButtonState();
        }

        /// <summary>
        /// Löscht die ausgewählten Kanäle
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void BtnDeleteChannelClick(object aSender, ItemClickEventArgs aE)
        {
            _measurementTreeList.DeleteSelectedNodes();
            SetButtonState();
        }

        /// <summary>
        /// Setzt den Status der Buttons
        /// </summary>
        private void SetButtonState()
        {
            bool enabled = _measurementTreeList.SelectedMeasurements.Count > 0;
            _nextBarButtonItem.Enabled = enabled;
            _removeChannelsBarButtonItem.Enabled = enabled;
            _removeConfigBarButtonItem.Enabled = enabled;
            _saveConfigBarButtonItem.Enabled = enabled;
        }

        /// <summary>
        /// Verarbeitet die Keys vom Benutzer
        /// </summary>
        /// <param name="aMessage">Wird nicht benötigt</param>
        /// <param name="aKeyData">Der Key der vom Benutzer gedrück wurde</param>
        /// <returns><c>true</c> wenn der Key verarbeitet wurde, sonst <c>false</c></returns>
        protected override bool ProcessCmdKey(ref Message aMessage, Keys aKeyData)
        {
            if (IdexNeoKeys.IsModuleAllowedKey(aKeyData))
            {
                switch (aKeyData)
                {
                    case Keys.Escape:
                        SwitchBack();
                        break;

                    case Keys.Enter:
                        SwitchNext();
                        break;

                    case Keys.Delete:
                        _measurementTreeList.DeleteSelectedNodes();
                        break;

                    default:
                        return base.ProcessCmdKey(ref aMessage, aKeyData);
                }

                return true;
            }

            return base.ProcessCmdKey(ref aMessage, aKeyData);
        }

        /// <summary>
        /// Schält zum nächsten Panel weiter
        /// </summary>
        private void SwitchNext()
        {
            if (_measurementTreeList.SelectedMeasurements.Count > 0)
            {
                ExecuteButtonFunctions?.Invoke(PanelControlStates.Next, _measurementTreeList.SelectedMeasurements);
            }
        }

        /// <summary>
        /// Schält zurück zur Funktionsliste
        /// </summary>
        private void SwitchBack()
        {
            ExecuteButtonFunctions?.Invoke(PanelControlStates.SwitchBack);
        }

        /// <summary>
        /// Methode wird ausgelöst, sobald sich der Text im Channel Input ändert.
        /// </summary>
        /// <param name="aSender">Der Sender des Events.</param>
        /// <param name="aE">Die Eventargumente.</param>
        private void OnChannelNrEdit_EditValueChanging(object aSender, ChangingEventArgs aE)
        {
            _addChannelBarButtonItem.Enabled = !string.IsNullOrWhiteSpace((string) aE.NewValue);
            _addChannelBarButtonItem.Refresh();
        }
    }
}
