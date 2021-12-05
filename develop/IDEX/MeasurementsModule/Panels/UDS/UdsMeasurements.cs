namespace MeasurementsModule.Panels.UDS
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ActionInfoModule;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Enum;
    using De.VwAudi.Idex.Common.Gui.Helpers;
    using DevExpress.XtraBars;
    using Enum;
    using Strings;
    using Utils;
    using EventHandler = Events.EventHandler;

    internal sealed partial class UdsMeasurements : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Konstruktor zum initialisieren der GUI Klasse
        /// </summary>
        public UdsMeasurements()
        {
            InitializeComponent();

            //Diese Eigenschaft MUSS auf true gesetzt werden, da sonst keine Daten im GridView angezeigt werden.
            layoutControl1.UseLocalBindingContext = true;
            _measurementTreeList.InitSettingsByProtocol(Protocol.UdsOnCan);
            _measurementTreeList.OnButtonStateChanged += SetButtonState;
            _searchEdit.Client = _measurementTreeList.GetTreeListAsClient;
        }

        /// <summary>
        /// Delegate zum schreiben von ForwardMessageToActionInfo Meldungen
        /// </summary>
        internal EventHandler.ActionInfoEventHandler ActionInfo { get; set; }

        /// <summary>
        /// Delegate-Object zum weiterleiten der Buttonfunktionen
        /// </summary>
        internal EventHandler.PanelControlDelegate ButtonController { get; set; }

        /// <summary>
        /// Setzt den Focus in die TreeList
        /// </summary>
        internal void SetFocus() => _measurementTreeList.Focus();

        /// <summary>
        /// Aktualisiert die TreeList
        /// </summary>
        /// <param name="aMeasurementList"></param>
        public void UpdateDataSource(IEnumerable<IPrimitiveParameter> aMeasurementList) => _measurementTreeList.UpdateMeasurementTreeList(aMeasurementList);

        /// <summary>
        /// Button zum Zurück Schalten
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void SBtnSwitchBackClicked(object aSender, ItemClickEventArgs aE) => SwitchBack();

        /// <summary>
        /// Steuert die Bedienbarkeit des weiter-Buttons
        /// </summary>
        /// <param name="aOffset">Offset</param>
        private void SetButtonState(int aOffset = 0) => _nextBarButtonItem.Enabled = _measurementTreeList.SelectedMeasurements.Count + aOffset > 0;

        /// <summary>
        /// Zeigt die Odx-Namen in der GUI an.
        /// </summary>
        internal void ShowOdxNames(bool aShow) => _measurementTreeList.ShowOdxNames(aShow);

        /// <summary>
        /// Zeigt die Service-IDs in der GUI an.
        /// </summary>
        internal void ShowServiceIdentifier(bool aShow) => _measurementTreeList.ShowServiceIdentifier(aShow);

        /// <summary>
        /// Schält zum auslesen der Messwerte weiter
        /// </summary>
        /// <param name="aSender">Parameter wird nicht benutzt.</param>
        /// <param name="aE">Parameter wird nicht benutzt.</param>
        private void SBtnNextClick(object aSender, ItemClickEventArgs aE) => SwitchNext();

        /// <summary>
        /// Speichert die Konfiguration
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void SBtnConfigurationSaveClick(object aSender, ItemClickEventArgs aE)
        {
            if (ConfigManager.SaveConfiguration(_measurementTreeList.SelectedMeasurements))
            {
                ActionInfo(aSender, Resource.ConfigurationSavedSuccessfully, ActionInfoType.Info);
            }
            else
            {
                ActionInfo(aSender, Resource.ConfigurationNotSaveable, ActionInfoType.Error);
            }
        }

        /// <summary>
        /// Lädt eine Konfiguration
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void SBtnConfigurationLoadClick(object aSender, ItemClickEventArgs aE)
        {
            var measuredValuesToSelect = ConfigManager.LoadConfiguration();

            if (measuredValuesToSelect == null)
            {
                ActionInfo(aSender, Resource.ConfigurationNotLoadable, ActionInfoType.Error);
                return;
            }

            var errorLst = _measurementTreeList.SelectMeasuredValues(measuredValuesToSelect);
            _measurementTreeList.RefreshDataSource();
            SetButtonState();

            if (errorLst.Count == 0)
            {
                ActionInfo(aSender, Resource.ConfigurationLoadedSuccessfully, ActionInfoType.Info);
            }
            else
            {
                foreach (var error in errorLst)
                {
                    ActionInfo(aSender, $"{Resource.MeasurementNotLoadable} - {error.DisplayName}", ActionInfoType.Warning);
                }
            }
        }

        /// <summary>
        /// Löscht die Konfiguration
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void SBtnConfigurationRemoveClick(object aSender, ItemClickEventArgs aE)
        {
            _measurementTreeList.SetSelectionOfMeasuredValues(false);
            _measurementTreeList.RefreshDataSource();
            ActionInfo(aSender, Resource.ConfigurationRemoved, ActionInfoType.Info);
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
                ButtonController?.Invoke(PanelControlStates.Next, _measurementTreeList.SelectedMeasurements);
            }
        }

        /// <summary>
        /// Schält zurück zur Funktionsliste
        /// </summary>
        private void SwitchBack()
        {
            _measurementTreeList.UpdateMeasurementTreeList(new List<IPrimitiveParameter>(), true);
            ButtonController?.Invoke(PanelControlStates.SwitchBack);
        }

        /// <summary>
        /// Selektiert/Deselektiert alle Messwerte
        /// </summary>
        /// <param name="aSender">Parameter wird nicht verwendet.</param>
        /// <param name="aE">Parameter wird nicht verwendet.</param>
        private void BtnSelectAllMeasurementsClicked(object aSender, ItemClickEventArgs aE) => _measurementTreeList.SelectOrDeselectAllMeasurements();
    }
}
