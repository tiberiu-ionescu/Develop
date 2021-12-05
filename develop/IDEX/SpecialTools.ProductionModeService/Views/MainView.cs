namespace SpecialTools.ProductionModeService.Views
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using De.VwAudi.Idex.Common.Gui.Util;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Grid;
    using EventArguments;
    using IdexCommonCommunication.Interaction;
    using Messaging.ProductionMode.Responses;
    using Properties;
    using ViewModels;

    /// <summary>
    /// Hauptansicht von dem Tool
    /// </summary>
    internal sealed partial class MainView : XtraUserControl
    {
        /// <summary>
        /// Overall State Label
        /// </summary>
        private const string OVERALL_STATE_LABEL = "Status: ";

        /// <summary>
        /// Result Type Column Field Name
        /// </summary>
        private const string RESULT_TYPE_COLUMN_FIELD_NAME = "LocalizedResult";

        /// <summary>
        /// Transparenzfarbe
        /// </summary>
        private static readonly Color TRANSPARENCY_COLOR = Color.FromArgb(150, 50, 50, 50);

        /// <summary>
        /// Referenz auf den MainViewModel zum modifizierten des MainViews
        /// </summary>
        private readonly MainViewModel _mainViewModel;

        /// <summary>
        /// Buttons sind vefügbar, ein neuer Ablauf kann gestartet werden
        /// </summary>
        private bool _isAvailable = true;

        /// <summary>
        /// Anfragetype
        /// </summary>
        private string _requestType;

        /// <summary>
        /// Warning message text
        /// </summary>
        private string _warningMessageText = Resources.COULD_NOT_CONNECT_TO_GATEWAY;

        /// <summary>
        /// Konstruktor
        /// </summary>
        internal MainView()
        {
            Cancellation = new CancellationTokenSource();

            _mainViewModel = new MainViewModel();

            InitializeComponent();
            layoutControlItem2.ControlAlignment = ContentAlignment.MiddleCenter;
            _mainViewModel.FocusReceived += (aEventArgs, aCommand) =>  Context.RunInUiContext(() => FocusReceived());
            gridControlPModeStatus.DataSource = _mainViewModel._ecuProductionModeStateList;
            gridControlPModeStatus.BindingContext = new BindingContext();
            _mainViewModel.ResponseReceivedEvent += ResponseReceived;
            _mainViewModel.NoProjectEvent += OnNoProjectEvent;
            _mainViewModel.ProductionModeNotSupportedEvent += OnProductionModeNotSupportedEvent;
            SizeChanged += (aSender, aEventArgs) => UpdateWarningPosition();
            gridViewPModeStatus.GroupPanelText = OVERALL_STATE_LABEL;
            gridViewPModeStatus.RowCellStyle += HighlightColumns;
        }

        /// <summary>
        /// CancellationTokenSource
        /// </summary>
        internal CancellationTokenSource Cancellation { get; }

        /// <summary>
        /// Initialisiert das Main View für den ersten Start
        /// </summary>
        internal void Initialize()
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
            }

            FocusReceived();
        }

        /// <summary>
        /// Behandelt einen Klick auf den Activate Button
        /// </summary>
        /// <param name="aSender">Sender</param>
        /// <param name="aE">Event Argumente</param>
        private void ActivateButtonClick(object aSender, EventArgs aE)
        {
            _mainViewModel.RequestActivateProductionMode();
            _requestType = Resources.ACTIVATING;
            SetUiBusy();
        }

        /// <summary>
        /// Sendet einen Broadcast beim Button Click
        /// </summary>
        /// <param name="aSender">Sender</param>
        /// <param name="aEventArgs">Event Argumente</param>
        private void CheckButtonClick(object aSender, EventArgs aEventArgs)
        {
            CheckProductionMode();
        }

        /// <summary>
        /// Liest den Zustand des Produktionsmodus und modifiziert entsprechend die Button Leiste
        /// </summary>
        private void CheckProductionMode()
        {
            _mainViewModel.QueryReadProductionMode();
            _requestType = Resources.CHECKING;
            SetUiBusy();
        }

        /// <summary>
        /// Sendet einen Broadcast beim Button Click
        /// </summary>
        /// <param name="aSender">Sender</param>
        /// <param name="aEventArgs">Event Argumente</param>
        private void DeactivateButtonClick(object aSender, EventArgs aEventArgs)
        {
            _mainViewModel.RequestDeactivateProductionMode();
            _requestType = Resources.DEACTIVATING;
            SetUiBusy();
        }

        /// <summary>
        /// Schaltflächen deaktivieren
        /// </summary>
        private void SetUiBusy()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(SetUiBusy));
            }
            else
            {
                var token = Cancellation.Token;
                _checkButton.Enabled = false;
                _activateButton.Enabled = false;
                _deactivateButton.Enabled = false;
                _progressBar.Visible = true;
                _isAvailable = false;
                _progressBarLayoutControItem.ContentVisible = true;
                UpdateOverallStatusText(_requestType);
                Task.Run(() => UpdateOverallStatusText(_requestType), token);
            }
        }

        /// <summary>
        /// Schaltflächen aktivieren
        /// </summary>
        private void SetUiAvailable()
        {
            _checkButton.Enabled = true;
            _activateButton.Enabled = true;
            _deactivateButton.Enabled = true;
            _isAvailable = true;
            _progressBar.Visible = false;
            _progressBarLayoutControItem.ContentVisible = false;
            UpdateOverallStatusText(_mainViewModel._overallProductionModeState.ToString());
        }

        /// <summary>
        /// Fokus wurde erhalten
        /// </summary>
        private void FocusReceived()
        {
            if(_isAvailable)
            {
                CheckProductionMode();
            }

            Context.RunInUiContext(HideWarning);
        }

        /// <summary>
        /// Versteckt die Warnung
        /// </summary>
        private void HideWarning()
        {
            _transparencyPanel.SendToBack();
            _transparencyPanel.Visible = false;
        }

        /// <summary>
        /// Conditional formating of columns in Data Grid
        /// </summary>
        /// <param name="aSender">Ignored</param>
        /// <param name="aEventArgs">RowCellStyle event's arguments</param> 
        private void HighlightColumns(object aSender, RowCellStyleEventArgs aEventArgs)
        {
            var view = aSender as GridView;
            if (aEventArgs.Column.FieldName == RESULT_TYPE_COLUMN_FIELD_NAME)
            {
                if (view != null && (string) view.GetRowCellValue(aEventArgs.RowHandle, RESULT_TYPE_COLUMN_FIELD_NAME) != Resources.RESULT_NO_ERROR)
                {
                    aEventArgs.Appearance.ForeColor = Color.Crimson;
                }
                else
                {
                    aEventArgs.Appearance.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Das Message Gateway teilt mit, dass kein Projekt geladen ist
        /// </summary>
        /// <param name="aSender">Ignored</param>
        /// <param name="aEventArgs">Ignored</param>
        private void OnNoProjectEvent(object aSender, ResponseReceivedEventArgs<IRequest, NoProjectResult> aEventArgs)
        {
            _warningMessageText = Resources.NO_PROJECT;
            if (InvokeRequired)
            {
                Invoke(new Action(ShowWarning));
            }
            else
            {
                ShowWarning();
            }
        }

        /// <summary>
        /// Das Message Gateway teilt mit, dass Production Mode Not Supported ist
        /// </summary>
        /// <param name="aSender">Ignored</param>
        /// <param name="aEventArgs">Ignored</param>
        private void OnProductionModeNotSupportedEvent(object aSender, ResponseReceivedEventArgs<IRequest, ProductionModeNotSupportedResult> aEventArgs)
        {
            _warningMessageText = Resources.PRODUCTION_MODE_NOT_SUPPORTED;
            if (InvokeRequired)
            {
                Invoke(new Action(ShowWarning));
            }
            else
            {
                ShowWarning();
            }
        }

        /// <summary>
        /// Erneuert die Data Grid
        /// </summary>
        /// <param name="aSender">Ignored</param>
        /// <param name="aEventArgs">Event Argumente, die den Modifizierten Inhalt enthalten</param>
        private void ResponseReceived(object aSender, EventArgs aEventArgs)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(SetUiAvailable));
            }
            else
            {
                SetUiAvailable();
            }
        }

        /// <summary>
        /// Zeigt die Warnung
        /// </summary>
        private void ShowWarning()
        {
            _transparencyPanel.Initialize(TRANSPARENCY_COLOR, this);
            _transparencyPanel.BringToFront();
            _transparencyPanel.Visible = true;
            UpdateWarningPosition();
            _warningControl.SetText(_warningMessageText);
            _warningControl.BringToFront();
            _warningControl.Invalidate();
            Update();
        }

        /// <summary>
        /// Aktualisiert den Gesamtstatus-Text
        /// </summary>
        /// <param name="aTxt">Statustext</param>
        private void UpdateOverallStatusText(string aTxt) => Context.RunInUiContext(() => gridViewPModeStatus.GroupPanelText = OVERALL_STATE_LABEL + aTxt);

        /// <summary>
        /// Passt das Transparenz-Panel und die Position der Warning an
        /// </summary>
        private void UpdateWarningPosition()
        {
            _transparencyPanel.Size = Size;
            _transparencyPanel.Location = Point.Empty;
            _warningControl.Location = new Point(Width / 2 - _warningControl.Width / 2, Height / 2 - _warningControl.Height / 2);
        }
    }
}
