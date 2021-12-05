namespace MeasurementsModule.Panels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ActionInfoModule;
    using Data;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Diagnostic.ServicesResult.Measurement;
    using De.VwAudi.Diagnostic.Idex.Essentials.Interfaces.Enum;
    using De.VwAudi.Idex.Common.Gui.Data;
    using De.VwAudi.Idex.Common.Gui.Helpers;
    using De.VwAudi.Idex.Common.Gui.Util;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using Enum;
    using NLog;
    using Strings;
    using EventHandler = Events.EventHandler;
    using Timer = System.Timers.Timer;

    /// <summary>
    /// Klasse zeigt die ausgelesenen Messwertblöcke an
    /// </summary>
    internal sealed partial class MeasurementResults : XtraUserControl
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Aktives Diagnoseprotokoll
        /// </summary>
        private readonly Protocol _protocol;

        /// <summary>
        /// Timer für daszyklische Auslesen der Messwerte
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// CancellationTokenSource
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// Nummer des aktuellen Messwertes
        /// </summary>
        private int _measurementNo;

        /// <summary>
        /// Flag wird benötigt, um zu verhindern, dass der Timer Messwerte abfragt, wenn die vorherige Abfrage noch nicht zu Ende ist.
        /// </summary>
        private bool _preventTimerAction;

        /// <summary>
        /// Flag, ob Timer-Event bereits ausgelöst wurde, aber Auslesevorgang nicht gestartet werden konnte, weil ein anderere Messwerte Auslesevorgang noch lief.
        /// </summary>
        private bool _timerAlreadyElapsed = true;

        /// <summary>
        /// Speichert den Zustand des Timers (Run = true oder Stop = false)
        /// </summary>
        private bool _timerState;

        /// <summary>
        /// CancellationToken, zum abbrechen der Tasks
        /// </summary>
        private CancellationToken _token;

        /// <summary>
        /// Initialisiert das UserControl zum Anzeigen der gepollten Messwerte
        /// </summary>
        internal MeasurementResults(Protocol aProtocol)
        {
            InitializeComponent();

            _measurementTreeList.OnButtonStateChanged += SetButtonState;
            _measurementTreeList.Dock = DockStyle.Fill;
            _protocol = aProtocol;

            //Diese Eigenschaft MUSS auf true gesetzt werden, da sonst keine Daten im GridView angezeigt werden.
            layoutControl1.UseLocalBindingContext = true;

            _timer = new Timer();
            _timer.Elapsed += async delegate { await HandleTimerAsync().ConfigureAwait(true); };

            _measurementTreeList.Init(_protocol);
        }

        /// <summary>
        /// Delegate zum Schreiben von ForwardMessageToActionInfo Meldungen
        /// </summary>
        internal EventHandler.ActionInfoEventHandler ActionInfo { get; set; }

        /// <summary>
        /// Delegate-Object zum Weiterleiten der Buttonfunktionen
        /// </summary>
        internal EventHandler.PanelControlDelegate ButtonController { get; set; }

        /// <summary>
        /// Delegate zum Auslesen der Messwerte
        /// </summary>
        internal EventHandler.ReadMeasurementDelegate TriggerReadMeasurementsAsync { get; set; }

        /// <summary>
        /// Aktualisiert die TreeList  [UDS]
        /// </summary>
        /// <param name="aMeasurementList">Liste mit Messwerten</param>
        public void UpdateDataSource(IEnumerable<MeasurementDataEntry> aMeasurementList) => _measurementTreeList.UpdateDataSource(aMeasurementList);

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
                        SBtnStartClick(this, new ItemClickEventArgs(null, null));
                        break;

                    default:
                        return base.ProcessCmdKey(ref aMessage, aKeyData);
                }

                return true;
            }

            return base.ProcessCmdKey(ref aMessage, aKeyData);
        }

        /// <summary>
        /// Liest die selektierten UDS-Messwerte aus.
        /// </summary>
        internal async Task ReadMeasurementsAsync()
        {
            _measurementNo = 0;
            _measurementTreeList.SetReadingMeasurements(true);
            InitCancellationToken();
            _startBarButtonItem.Enabled = false;
            await TriggerReadMeasurementsAsync(_measurementTreeList.SelectedMeasurements, new Progress<IReadMeasuredBlockResult>(UpdateMeasurement), _token).ConfigureAwait(true);
            _measurementTreeList.SetReadingMeasurements(false);
            _startBarButtonItem.Enabled = true;

            OnInitialMeasurementFinished();
        }

        /// <summary>
        /// Setzt die Zykluszeit
        /// </summary>
        /// <param name="aCycleTime">Zykluszeit</param>
        internal void SetCycleTime(double aCycleTime) => _timer.Interval = aCycleTime;

        /// <summary>
        /// Setzt den Focus in die TreeList
        /// </summary>
        internal void SetFocus() => _measurementTreeList.Focus();

        /// <summary>
        /// Setzt die Modulkonfiguration
        /// </summary>
        /// <param name="aModuleConfig"></param>
        internal void SetModuleConfiguration(ModuleConfig aModuleConfig)
        {
            SetCycleTime(aModuleConfig.CycleTime);

            if (_protocol == Protocol.UdsOnCan)
            {
                ShowServiceIdentifier(aModuleConfig.ShowServiceIds);
                ShowOdxNames(aModuleConfig.ShowOdxNames);
            }
            else
            {
                ShowServiceIdentifier(false);
                ShowOdxNames(false);
            }
        }

        /// <summary>
        /// Zeigt die Odx-Namen in der GUI an.
        /// </summary>
        internal void ShowOdxNames(bool aShow) => _measurementTreeList.ShowOdxNames(aShow);

        /// <summary>
        /// Zeigt die Service-IDs in der GUI an.
        /// </summary>
        internal void ShowServiceIdentifier(bool aShow) => _measurementTreeList.ShowServiceIdentifier(aShow);

        /// <summary>
        /// Stopt das Auslesen der Messwerte
        /// </summary>
        internal void StopCommunication()
        {
            _cts?.Cancel();
            _timer.Stop();
            _timerState = false;
            _measurementTreeList.SetReadingMeasurements(false);
        }

        /// <summary>
        /// Liest die Messwerte aus, wenn der Timer das Event feuert.
        /// </summary>
        /// <returns></returns>
        private async Task HandleTimerAsync()
        {
            if (!_preventTimerAction && _timerAlreadyElapsed)
            {
                _timerAlreadyElapsed = false;
                _preventTimerAction = true;
                var measurements = await TriggerReadMeasurementsAsync(_measurementTreeList.SelectedMeasurements, new Progress<IReadMeasuredBlockResult>(UpdateMeasurement), _token).ConfigureAwait(true);

                if (MissingAllResponses(measurements))
                {
                    Context.RunInUiContext(() => StopReadingMeasurementsCyclically(Resource.MeasurementsReadingConnectionLost));
                }
                else
                {
                    _preventTimerAction = false;
                }

                await HandleTimerAsync().ConfigureAwait(true);
            }
            else
            {
                _timerAlreadyElapsed = true;
            }
        }

        /// <summary>
        /// Initialisiert das Cancellation Token
        /// </summary>
        private void InitCancellationToken()
        {
            _cts = new CancellationTokenSource();
            _token = _cts.Token;
        }

        /// <summary>
        /// Prüft, ob alle Antworten von den Steuergeräte fehlen. Möglicherweise handelt sich dann um das Verlieren der Fahrzeugverbindung
        /// </summary>
        /// <param name="aMeasurements">Services für die Messwerte nach der Ausführung</param>
        /// <returns>TRUE, wenn in alle Messwerte die Antwort fehlt. Anderenfalls, TRUE</returns>
        private static bool MissingAllResponses(IList<IReadMeasuredBlockResult> aMeasurements)
        {
            return aMeasurements.All(aE => aE.ErrorCase != ErrorCase.None);
        }

        /// <summary>
        /// Speichert die ausgelesenen Messwertblöcke in der VehicleData
        /// </summary>
        private void SaveMeasuredValueInVehicleData(IReadMeasuredBlockResult aMeasurement) => Task.Run(() => ButtonController(PanelControlStates.SaveInVehicleData, aMeasurement));

        /// <summary>
        /// Drucke Protokoll
        /// </summary>
        /// <param name="aSender">Wird nicht verwendet.</param>
        /// <param name="aEventArgs">Wird nicht verwendet.</param>
        private void SBtnPrintProtocolClick(object aSender, ItemClickEventArgs aEventArgs) => ButtonController(PanelControlStates.PrintProtocol);

        /// <summary>
        /// Startet das zyklische Auslesen der Messwerte
        /// </summary>
        /// <param name="aSender">Parameter wird nicht verwendet.</param>
        /// <param name="aE">Parameter wird nicht verwendet.</param>
        private void SBtnStartClick(object aSender, ItemClickEventArgs aE)
        {
            if (_timerState)
            {
                StopReadingMeasurementsCyclically(Resource.MeasurementsReadingStopped);
            }
            else
            {
                StartReadingMeasurementsCyclicallyAsync();
            }
        }

        /// <summary>
        /// Schaltet zurück zum Panel AvailableMeasurements
        /// </summary>
        /// <param name="aSender">Parameter wird nicht benutzt.</param>
        /// <param name="aE">Parameter wird nicht benutzt.</param>
        private void SBtnSwitchBackClick(object aSender, ItemClickEventArgs aE) => SwitchBack();

        /// <summary>
        /// Methode aktiviert/deaktiviert den Startbutton abhängig von der Anzahl der ausgewählten Messwerte. Es muss mindestens ein Messwert ausgewählt sein, um das Auslesen starten zu können.
        /// </summary>
        /// <param name="aOffset">[OPTIONAL] offset gibt an, ob von der Anzahl der ausgewählten Messwerte entweder einer abezogen oder hinzugefügt werden muss, da die Methode <i>vor</i> dem Ändern der </param>
        private void SetButtonState(int aOffset = 0) => _startBarButtonItem.Enabled = _measurementTreeList.SelectedMeasurements.Count + aOffset > 0;

        /// <summary>
        /// Setzt die ProgressBar
        /// </summary>
        /// <param name="aCount">Anzahl der Messwerte</param>
        private void SetProgressBar(int aCount)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker) (() => SetProgressBar(aCount)));
            }
            else
            {
                _progressRibbonPageGroup.Visible = !_cts.IsCancellationRequested && _measurementNo < aCount || _timerState;
                _progressBarEditItem.EditValue = $@"{Resource.ReadMeasurement} {_measurementNo}/{aCount}";
                _measurementNo %= aCount;
            }
        }

        /// <summary>
        /// Startet das zyklische Auslesen der Messwerte
        /// </summary>
        private async void StartReadingMeasurementsCyclicallyAsync()
        {
            if (_measurementTreeList.SelectedMeasurements.Count > 0)
            {
                InitCancellationToken();
                _measurementNo = 0;
                _measurementTreeList.SetReadingMeasurements(true);
                _timerState = true;
                _timer.Start();
                _startBarButtonItem.Caption = Resource.BtnStop;
                _startBarButtonItem.ImageOptions.SvgImage = _startButtonSvgImageCollection[1];
                _backBarButtonItem.Enabled = false;
                SetProgressBar(_measurementTreeList.SelectedMeasurements.Count);
                ActionInfo(this, Resource.MeasurementsReadingStarted, ActionInfoType.Info);
                await HandleTimerAsync().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Stoppt das zyklische Auslesen der Messwerte
        /// </summary>
        private void StopReadingMeasurementsCyclically(string aDisplayedMessage)
        {
            StopCommunication();
            _startBarButtonItem.Caption = Resource.BtnStart;
            _startBarButtonItem.ImageOptions.SvgImage = _startButtonSvgImageCollection[0];
            _backBarButtonItem.Enabled = true;
            _progressRibbonPageGroup.Visible = false;
            ActionInfo(this, aDisplayedMessage, ActionInfoType.Info);
        }

        /// <summary>
        /// Schält zurück zum Hauptpanel
        /// </summary>
        private void SwitchBack()
        {
            OnLeaveView();

            StopCommunication();
            ButtonController?.Invoke(PanelControlStates.MainPanel);
        }

        /// <summary>
        /// Aktualisiert die TreeList mit den ausgelesenen Daten
        /// </summary>
        /// <param name="aUpdatedMeasurement">Messwertblock mit ausgelesenen Messwerten.</param>
        private void UpdateMeasurement(IReadMeasuredBlockResult aUpdatedMeasurement)
        {
            var allowedShortNames = _measurementTreeList.SelectedMeasurements.Select(aMeasurement => aMeasurement.ShortName).ToList();
            if (allowedShortNames.Contains(aUpdatedMeasurement.RequestParameter, StringComparer.OrdinalIgnoreCase))
            {
                _measurementTreeList.UpdateMeasurementBlock(aUpdatedMeasurement);

                //Speichert/Aktualisiert die Messwerte in der VehicleData
                SaveMeasuredValueInVehicleData(aUpdatedMeasurement);
            }
            else
            {
                LOGGER.Warn($"an unexpected mwb was received: no mwb with shortname {aUpdatedMeasurement.MeasuredBlock.MwbBlock.Name} was requested");
            }

            _measurementNo++;
            SetProgressBar(_measurementTreeList.SelectedMeasurements.Count);
        }
    }
}
