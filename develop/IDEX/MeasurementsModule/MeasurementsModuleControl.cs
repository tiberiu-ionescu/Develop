/*
 * Copyright (c) AUDI AG 2017
 *
 * Autor: Funk, Jan (I/EE-45)
 * Erstellt: 20.2.2017
 */

namespace MeasurementsModule
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ActionInfoModule;
    using Core.Core;
    using Core.IdexBaseModule;
    using Core.Types;
    using CsvLogging;
    using Data;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic.ServicesResult.Measurement;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Enum;
    using De.VwAudi.Idex.Common.Gui.Control;
    using De.VwAudi.Idex.Common.Gui.Data;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraEditors;
    using Enum;
    using Panels;
    using Strings;

    /// <summary>
    /// Modul zum auslesen der Messwerte
    /// </summary>
    internal sealed partial class MeasurementsModuleControl : BaseModuleConfigurableControl
    {
        /// <summary>
        /// Kleinster Schritt, welcher mit der TimeTrackBar eingestellt werden kann.
        /// </summary>
        private const int TIME_TRACK_BAR_STEP = 50;

        /// <summary>
        /// Eine Sekunde in Millisekunden.
        /// </summary>
        private const double ONE_SECOND = 1000.0;

        /// <summary>
        /// Konstante definiert den Namen des Handbuchs für das Modul, mit einem Platzhalter für die jeweilige Sprache, die ausgewählt ist.
        /// </summary>
        private const string MANUAL_FILE_NAME = "Manual_Measurements_{0}.pdf";

        /// <summary>
        /// Name der Hilfedatei als Backup, falls die Culture nicht erfolgreich ausgelesen werden konnte. Sinnvoll?
        /// </summary>
        private const string MANUAL_FILE_NAME_BACKUP = "Manual_Measurements_DE.pdf";
        
        /// <summary>
        /// Verwaltet die Funktion um das Handbuch zu dem Modul anzuzeigen
        /// </summary>
        private ModuleInfoRibbon _moduleInfo = new ModuleInfoRibbon(MANUAL_FILE_NAME, MANUAL_FILE_NAME_BACKUP);

        /// <summary>
        /// Prozess-Klasse, für den Zugriff auf den Diagnose-Layer
        /// </summary>
        private readonly MeasurementsModuleProcess _process;

        /// <summary>
        /// Zugriff auf das ActionInfoModule
        /// </summary>
        private ActionInfoModuleControl _actionInfoModuleControl;

        /// <summary>
        /// Enthält die Konfiguration des Moduls
        /// </summary>
        private ModuleConfig _moduleConfig;

        /// <summary>
        /// Übernimmt die Steuerung der UserControls
        /// </summary>
        private PanelController _panelController;

        /// <summary>
        /// Module-Control-Klasse, zum Steuern der GUI
        /// </summary>
        /// <param name="aProcess"></param>
        public MeasurementsModuleControl(MeasurementsModuleProcess aProcess)
        {
            InitializeComponent();
            _process = aProcess;

            TopModuleName = ModuleConstants.SG_IDENT;
            BottomModuleName = ModuleConstants.ACTION_INFO;
            ControlLayout = ModuleLayout.TopMainBottom;

            ModuleRibbonPageCategory = _moduleRibbonPageCategory;
        }

        /// <summary>
        /// Initialisiert die ModuleControl-Klasse
        /// </summary>
        public override void Initialize()
        {
            Logger.Debug("Initializing...");

            CollapseOptionalPanel();

            _actionInfoModuleControl = (ActionInfoModuleControl) BottomControl;

            _moduleConfig = _process.LoadFromDisk<ModuleConfig>();

            Logger.Debug("Initialization done.");
        }

        /// <summary>
        /// Funktion, die ausgeführt wird, wenn das Modul aktiviert wird.
        /// </summary>
        /// <returns></returns>
        protected override async Task ActivateAsync()
        {
            //In der InitAsync() Methode wird anhand des Diagnoseprotokolls die richtige Diagnosklasse initialisiert.
            //Bei UDS werden zusätzlich noch die Messwerte vom ODX abgefragt. Da dies ein wenig Zeit in anspruch nimmt,
            //wird hier der Task gestartet und nicht gewartet. 
            var initProcessTask = _process.InitAsync().ConfigureAwait(false);
            _process.IsUdsBaseVariant(_actionInfoModuleControl.Warn);

            //Während die Messwerte ausgelesen werden, werden hier schonmal die Panels erzeugt.
            InitPanelController();
            AddControlsToMainPanel(_panelController.Panels);
            
            SetModuleConfiguration(_moduleConfig);
            SetOptionalPanelWidthAutomatically();

            await PanelControllerDelegateAsync(PanelControlStates.MainPanel, null).ConfigureAwait(false);

            //Warten, bis das Auslesen der Messwerte beendet ist.
            var isOk = await initProcessTask;

            if (isOk)
            {
                await PanelControllerDelegateAsync(PanelControlStates.ReadAvailableMeasurements, null).ConfigureAwait(false);
                ForwardMessageToActionInfoEvent(Resource.Ready, ActionInfoType.Info);
            }
            else
            {
                ForwardMessageToActionInfoEvent(Resource.ProtocolIsNotSupported, ActionInfoType.Error);
            }
        }

        /// <summary>
        /// Methode fügt Controls der aktiven Hauptansicht hinzu
        /// </summary>
        /// <param name="aControls">Die hinzuzufügenden Controls.</param>
        private void AddControlsToMainPanel(Control[] aControls)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => AddControlsToMainPanel(aControls)));
            }
            else
            {
                mainModulePanel.Controls.AddRange(aControls);
            }
        }

        /// <summary>
        /// Initialisiert den Panelcontroller
        /// </summary>
        private void InitPanelController()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(InitPanelController));
            }
            else
            {
                _panelController = new PanelController(_process.ActiveProtocol, AddRibbonPageGroupsToControlRibbonPage);
                _panelController.PanelControllerDelegate += PanelControllerDelegateAsync;
                _panelController.MeasurementReader += ReadMeasurementsAsync;
                _panelController.ForwardMessageToActionInfo += WriteToActionInfo; 
            }
        }
        
        /// <summary>
        /// Fügt die gewünschten <see cref="RibbonPageGroup"/> dem Ribbon hinzu.
        /// Die bereits angezeigten Gruppen werden aus der Anzeige entfernt.
        /// </summary>
        /// <param name="aRibbonPageGroups">Gruppen die hinzugefügt werden</param>
        private void AddRibbonPageGroupsToControlRibbonPage(RibbonPageGroup[] aRibbonPageGroups)
        {
            _controlRibbonPage.Groups.Clear();
            _controlRibbonPage.Groups.AddRange(aRibbonPageGroups);
            _controlRibbonPage.Groups.Add(_moduleInfo.GetHelpRibbonPageGroup());
        }

        /// <summary>
        /// Setzt die Modulkonfiguration
        /// </summary>
        private void SetModuleConfiguration(ModuleConfig aModuleConfig)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetModuleConfiguration(aModuleConfig)));
            }
            else
            {
                _tabView.PageVisible = _process.ActiveProtocol == Protocol.UdsOnCan;

                if (aModuleConfig == null)
                {
                    aModuleConfig = new ModuleConfig
                    {
                        ShowOdxNames = false,
                        ShowServiceIds = false,
                        CycleTime = 1000
                    };
                }

                ShowOdxNames.Checked = aModuleConfig.ShowOdxNames;
                ShowServiceID.Checked = aModuleConfig.ShowServiceIds;
                CycleTimeTrackBar.EditValue = aModuleConfig.CycleTime;
                CycleTime.Text = GetCycleTimeText(aModuleConfig.CycleTime); 
            }
        }

        /// <summary>
        /// Leitet die Info-Meldungen an das WriteToActionInfo-Fenster weiter
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aMessage">Meldung</param>
        /// <param name="aActionInfoType">Typ der Meldung</param>
        private void WriteToActionInfo(object aSender, string aMessage, ActionInfoType aActionInfoType) => ForwardMessageToActionInfoEvent(aMessage, aActionInfoType);

        /// <summary>
        /// Liest die Messwerte aus
        /// </summary>
        /// <param name="aMeasurementList">Liste mit Messwerten</param>
        /// <param name="aUpdatedMeasurement">Progress zum aktualisieren der GUI</param>
        /// <param name="aCancellation">Cancellation Token</param>
        /// <returns></returns>
        private async Task<IList<IReadMeasuredBlockResult>> ReadMeasurementsAsync(IList<MeasurementDataEntry> aMeasurementList, IProgress<IReadMeasuredBlockResult> aUpdatedMeasurement, CancellationToken aCancellation)
        {
            Logger.Debug("Launching Read Measured Values in NG. "+aMeasurementList.Count+" responses are expected");
            var measurements = await _process.ReadMeasuredValuesAsync(aMeasurementList, aUpdatedMeasurement, aCancellation).ConfigureAwait(true);
            Logger.Debug(measurements.Count+" responses recieved.");

            if (!aCancellation.IsCancellationRequested)
            {
                await CsvLogger.AddDataToWriteAsync(aMeasurementList).ConfigureAwait(true);
            }

            return measurements;
        }

        /// <summary>
        /// Funktion, die ausgeführt wird, wenn das Modul deaktiviert wird.
        /// </summary>
        public override void Deactivate()
        {
            _moduleConfig = GetModuleConfiguration();
            _panelController.StopCommunication();
        }

        /// <summary>
        /// Liefert die Konfiguration des Moduls von der GUI
        /// </summary>
        /// <returns>Konfiguration des Moduls</returns>
        private ModuleConfig GetModuleConfiguration() => new ModuleConfig
                                                         {
                                                             ShowOdxNames = ShowOdxNames.Checked,
                                                             ShowServiceIds = ShowServiceID.Checked,
                                                             CycleTime = Convert.ToInt32(CycleTimeTrackBar.EditValue)
                                                         };

        /// <summary>
        /// Funktion, die ausgeführt wird, wenn das Modul geschlossen wird.
        /// </summary>
        public override void Shutdown() => _process.SaveToDisk(_moduleConfig);

        /// <summary>
        /// Leitet die Meldungen an das WriteToActionInfo-Fenster weiter
        /// </summary>
        /// <param name="aMessage">Meldung</param>
        /// <param name="aActionInfoType">Typ der Meldung</param>
        private void ForwardMessageToActionInfoEvent(string aMessage, ActionInfoType aActionInfoType)
        {
            switch (aActionInfoType)
            {
                case ActionInfoType.Error:
                    _actionInfoModuleControl.Error(aMessage);
                    break;
                case ActionInfoType.Info:
                    _actionInfoModuleControl.Info(aMessage);
                    break;
                case ActionInfoType.Warning:
                    _actionInfoModuleControl.Warn(aMessage);
                    break;
            }
        }

        /// <summary>
        /// Führt die verschiedenen Button-Funktionen der Panels aus
        /// </summary>
        /// <param name="aPanelControlStatesState">Status</param>
        /// <param name="aObject">Statusspezifisches Objekt</param>
        private async Task PanelControllerDelegateAsync(PanelControlStates aPanelControlStatesState, object aObject)
        {
            switch (aPanelControlStatesState)
            {
                case PanelControlStates.SwitchBack:
                    Deactivate();
                    OnSwitchToParentModule();
                    break;
                case PanelControlStates.SaveInVehicleData:
                    _process.SaveMeasuredValueInVehicleData((IReadMeasuredBlockResult) aObject);
                    break;
                case PanelControlStates.MainPanel:
                    _panelController.ShowMainPanel();
                    _panelController.SetModuleConfiguration(GetModuleConfiguration());
                    break;
                case PanelControlStates.Next:
                    await _panelController.ShowResultMeasurementsAsync((IEnumerable<MeasurementDataEntry>) aObject).ConfigureAwait(true);
                    _panelController.SetModuleConfiguration(GetModuleConfiguration());
                    break;

                case PanelControlStates.PrintProtocol:
                    _process.PrintProtocol();
                    break;
                case PanelControlStates.ReadAvailableMeasurements:
                    _panelController.UpdateDataSource(_process.GetAvailableUdsMeasurements());
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(aPanelControlStatesState), aPanelControlStatesState, null);
            }
        }

        /// <summary>
        /// Zeigt die ODX-Namen an
        /// </summary>
        /// <param name="aSender">CheckBox ShowOdxNames</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void ShowOdxNamesCheckedChanged(object aSender, EventArgs aE) => _panelController.ShowOdxNames(((CheckEdit) aSender).Checked);

        /// <summary>
        /// Zeigt die Service-Identifier in der GUI an.
        /// </summary>
        /// <param name="aSender">CheckBox ShowServiceIds</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void ShowServiceIDCheckedChanged(object aSender, EventArgs aE) => _panelController.ShowServiceIdentifier(((CheckEdit) aSender).Checked);

        /// <summary>
        /// Event wird ausgelöst, wenn sich der Schieberegler ändert.
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aE">Wird nicht verwendet.</param>
        private void CycleTimeEditValueChanged(object aSender, EventArgs aE)
        {
            var timeTrackBarValue = Convert.ToDouble(CycleTimeTrackBar.EditValue);
            var cycleTime = timeTrackBarValue - timeTrackBarValue % TIME_TRACK_BAR_STEP;
            CycleTimeTrackBar.EditValue = cycleTime;
            CycleTime.Text = GetCycleTimeText(cycleTime);
            _panelController.SetCycleTime(cycleTime);
        }

        /// <summary>
        /// Gibt die Zykluszeit mit Einheit als Text zurück
        /// </summary>
        /// <param name="aCycleTime">Zykluszeit</param>
        /// <returns></returns>
        private static string GetCycleTimeText(double aCycleTime) => aCycleTime < ONE_SECOND ? $"{aCycleTime} ms" : $"{aCycleTime / 1000} s";
    }
}
