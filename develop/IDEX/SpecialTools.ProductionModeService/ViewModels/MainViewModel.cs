// Copyright (c) AUDI AG 2020
// 
// Autor: Josovic, Nenad (I/EE-87, extern)
// Erstellt: 01.02.2021

namespace SpecialTools.ProductionModeService.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using De.VwAudi.Idex.Common.Gui.Util;
    using DevExpress.Mvvm.DataAnnotations;
    using EventArguments;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;
    using Messaging.ActionInfo;
    using Messaging.ProductionMode.Progress;
    using Messaging.ProductionMode.Responses;
    using Messaging.ProductionMode.Types;
    using Messaging.ToolGotFocus;
    using Models;
    using Properties;
    using Types;

    /// <summary>
    /// ViewModel zu dem MainView
    /// </summary>
    [PublicAPI]
    [POCOViewModel]
    public sealed class MainViewModel
    {
        /// <summary>
        /// Wird ausgelöst, wenn eine Command Tool Got Focus empfangen wird
        /// </summary>
        internal event EventHandler<CommandToolGotFocus> FocusReceived;

        /// <summary>
        /// Event wird ausgelöst, wenn eine No Project Result empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, NoProjectResult>> NoProjectEvent
        {
            add { _model.NoProjectResultReceived += value; }
            remove { _model.NoProjectResultReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Not Supported Result empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeNotSupportedResult>> ProductionModeNotSupportedEvent
        {
            add { _model.ProductionModeNotSupportedResultReceived += value; }
            remove { _model.ProductionModeNotSupportedResultReceived -= value; }
        }

        /// <summary>
        /// Wird ausgelöst, wenn eine Erfolgsantwort empfangen wird
        /// </summary>
        internal event EventHandler ResponseReceivedEvent;

        /// <summary>
        /// Datasource of Response Grid control
        /// </summary>
        internal BindingList<EcuState> _ecuProductionModeStateList = new BindingList<EcuState>();

        /// <summary>
        /// Overall Production Mode State
        /// </summary>
        internal ProductionModeState _overallProductionModeState;

        /// <summary>
        /// Referenz auf das Model
        /// </summary>
        private MainModel _model;

        /// <summary>
        /// Erstellt das Haupt-Viewmodel
        /// </summary>
        internal MainViewModel()
        {
            _model = new MainModel();
            _model.ToolGotFocusReceived += OnToolGotFocusReceived;
            _model.ProductionModeActivationResponseReceived += OnProductionModeActivationResponseReceived;
            _model.ProductionModeDeactivationResponseReceived += OnProductionModeDeactivationResponseReceived;
            _model.ReadProductionModeResultReceived += OnReadProductionModeResultReceived;
            _model.ProductionModeEcuListProgressReceived += OnProductionModeEcuListProgressReceived;
            _model.ProductionModeEcuStatusProgressReceived += OnProductionModeEcuStatusProgressReceived;
            _model.ProductionModeEcuModificationStartedProgressReceived += OnEcuModificationStarted;
        }

        /// <summary>
        /// Setzen des Produktionsmodus wurde auf einem Steuergerät gestartet
        /// </summary>
        /// <param name="aSender">Ignored</param>
        /// <param name="aEventArgs">Event Argumente mit Progress und dazugehörigem Progress</param>
        private void OnEcuModificationStarted(object aSender, ResponseReceivedEventArgs<IRequest, ProductionModeEcuModificationStartedProgress> aEventArgs)
        {
            var matchingEcu = _ecuProductionModeStateList.FirstOrDefault(aEcu => aEcu.BaudAddress == aEventArgs.Response.BaudAddress);
            if (matchingEcu != null)
            {
                string localizedTargetStatus;
                switch (aEventArgs.Response.TargetState)
                {
                    case ProductionModeState.Active:
                        localizedTargetStatus = Resources.STATE_ACTIVE;
                        break;
                    case ProductionModeState.Inactive:
                        localizedTargetStatus = Resources.STATE_INACTIVE;
                        break;
                    default:
                        localizedTargetStatus = Resources.STATE_UNKNOWN;
                        break;
                }
                DisplayActionInfo(string.Format(Resources.ACTION_INFO_ECU_MODIFICATION_STARTED,matchingEcu.LocalizedEcuName,localizedTargetStatus),ActionInfoLogLevel.Info);
            }
        }

        /// <summary>
        /// Query Read Production Mode
        /// </summary>
        internal void QueryReadProductionMode()
        {
            DisplayActionInfo(Resources.ACTION_INFO_READING,ActionInfoLogLevel.Info);
            _model.QueryReadProductionMode();
            LockMenuBar();
        }

        /// <summary>
        /// Request Activate Production Mode
        /// </summary>
        internal void RequestActivateProductionMode()
        {
            DisplayActionInfo(Resources.ACTION_INFO_ACTIVATING,ActionInfoLogLevel.Info);
            _model.RequestProductionModeActivate();
            LockMenuBar();
        }

        /// <summary>
        /// Request Deactivate Production Mode
        /// </summary>
        internal void RequestDeactivateProductionMode()
        {
            DisplayActionInfo(Resources.ACTION_INFO_DEACTIVATING,ActionInfoLogLevel.Info);
            _model.RequestProductionModeDeactivate();
            LockMenuBar();
        }

        /// <summary>
        /// Zeigt etwas auf der Action Info an
        /// </summary>
        /// <param name="aActionInfoMessage">Action Info Nachricht</param>
        /// <param name="aLogLevel">Log Level der Action Info</param>
        private void DisplayActionInfo(string aActionInfoMessage, ActionInfoLogLevel aLogLevel) => _model.DisplayActionInfo(aActionInfoMessage, aLogLevel);

        /// <summary>
        /// Sperrt die Menüleiste
        /// </summary>
        private void LockMenuBar() => _model.LockMenuBar();

        /// <summary>
        /// Schaltet die Menüleiste wiede frei
        /// </summary>
        private void UnlockMenuBar() => _model.UnlockMenuBar();

        /// <summary>
        /// Legt die Werte in der Bindungsliste basierend auf den empfangenen "Production Mode Activated Response" fest
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aResponseReceivedArgs">Erhalt einer regulären Antwort auf einen Production Mode Activation Request</param>
        private void OnProductionModeActivationResponseReceived(object aSender, ResponseReceivedEventArgs<IRequest, ProductionModeActivationResponse> aResponseReceivedArgs)
        {
            Context.RunInUiContext(() =>
                                   {
                                       _overallProductionModeState = aResponseReceivedArgs.Response.OverallResult;
                                       _ecuProductionModeStateList.Clear();
                                       foreach (var state in aResponseReceivedArgs.Response.CurrentProductionModeState.OrderBy((aProductionModeState)=> aProductionModeState.BaudAddress))
                                       {
                                           _ecuProductionModeStateList.Add(new EcuState(state));
                                       }
                                   });
            DisplayActionInfo(Resources.ACTION_INFO_ACTIVATED,ActionInfoLogLevel.Info);
            ResponseReceivedEvent?.Invoke(this, null);
            UnlockMenuBar();
        }

        /// <summary>
        /// Legt die Werte in der Bindungsliste basierend auf den empfangenen "Production Mode Deactivated Response" fest
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aResponseReceivedArgs">Erhalt einer regulären Antwort auf einen Production Mode Deactivation Request</param>
        private void OnProductionModeDeactivationResponseReceived(object aSender, ResponseReceivedEventArgs<IRequest, ProductionModeDeactivationResponse> aResponseReceivedArgs)
        {
            Context.RunInUiContext(() =>
                                   {
                                       _overallProductionModeState = aResponseReceivedArgs.Response.OverallResult;
                                       _ecuProductionModeStateList.Clear();
                                       foreach (var state in aResponseReceivedArgs.Response.CurrentProductionModeState.OrderBy((aProductionModeState)=> aProductionModeState.BaudAddress))
                                       {
                                           _ecuProductionModeStateList.Add(new EcuState(state));
                                       }
                                   });
            DisplayActionInfo(Resources.ACTION_INFO_DEACTIVATED,ActionInfoLogLevel.Info);
            ResponseReceivedEvent?.Invoke(this, null);
            UnlockMenuBar();
        }

        /// <summary>
        /// Legt die Werte in der Bindungsliste basierend auf den empfangenen "Production Mode Ecu List Progress" fest
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aResponseReceivedArgs">Erhalt einer regulären Antwort auf einen Production Mode Ecu List Progress</param>
        private void OnProductionModeEcuListProgressReceived(object aSender, ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress> aResponseReceivedArgs)
        {
            Context.RunInUiContext(() =>
                                   {
                                       _ecuProductionModeStateList.Clear();
                                       foreach (var state in aResponseReceivedArgs.Response.CurrentProductionModeState.OrderBy((aProductionModeState)=> aProductionModeState.BaudAddress))
                                       {
                                           _ecuProductionModeStateList.Add(new EcuState(state));
                                       }
                                   });
            DisplayActionInfo(string.Format(Resources.ACTION_INFO_SETTING_FOR_X_ECUS,aResponseReceivedArgs.Response.CurrentProductionModeState.Count),ActionInfoLogLevel.Info);
        }

        /// <summary>
        /// Aktualisiert die Werte in der Bindungsliste für den Eintrag mit derselben Baudadresse
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aResponseReceivedArgs">Erhalt einer regulären Antwort auf einen Production Mode Ecu Status Progress</param>
        private void OnProductionModeEcuStatusProgressReceived(object aSender, ResponseReceivedEventArgs<IRequest, ProductionModeEcuStatusProgress> aResponseReceivedArgs)
        {
            if (aResponseReceivedArgs.Response.ChangedEcu != null)
            {
                var ecuState = new EcuState(aResponseReceivedArgs.Response.ChangedEcu, aResponseReceivedArgs.Response.WaitingForMasterFlip);
                Context.RunInUiContext(() =>
                                       {
                                           var ecuProductionModeState = _ecuProductionModeStateList?.FirstOrDefault(aEntry => aEntry.BaudAddress == aResponseReceivedArgs.Response.ChangedEcu.BaudAddress);
                                           if (ecuProductionModeState != null)
                                           {
                                               var indexOf = _ecuProductionModeStateList.IndexOf(_ecuProductionModeStateList.FirstOrDefault(aEntry => aEntry.BaudAddress == aResponseReceivedArgs.Response.ChangedEcu.BaudAddress));
                                               _ecuProductionModeStateList[indexOf] = ecuState;
                                           }
                                       });
                if(aResponseReceivedArgs.Response.ChangedEcu.ResultType== ProductionModeResultType.NoError)
                {
                    if(aResponseReceivedArgs.Response.WaitingForMasterFlip)
                    {
                        DisplayActionInfo(string.Format(Resources.ACTION_INFO_WAITING_FOR_MASTER,ecuState.LocalizedEcuName),ActionInfoLogLevel.Info);
                    }
                    else
                    {
                        DisplayActionInfo(string.Format(Resources.ACTION_INFO_ECU_IN_STATE,ecuState.LocalizedEcuName,ecuState.LocalizedStatus),ActionInfoLogLevel.Info);
                    }
                }
                else
                {
                    DisplayActionInfo(string.Format(Resources.ACTION_INFO_ECU_FAIL,ecuState.LocalizedEcuName,ecuState.LocalizedResult),ActionInfoLogLevel.Warn);
                }
            }
        }

        /// <summary>
        /// Legt die Werte in der Bindungsliste basierend auf den empfangenen "Read Production Mode Result" fest
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aResponseReceivedArgs">Erhalt einer regulären Antwort auf einen Read Production Mode Query</param>
        private void OnReadProductionModeResultReceived(object aSender, ResponseReceivedEventArgs<IRequest, ReadProductionModeResult> aResponseReceivedArgs)
        {
            Context.RunInUiContext(() =>
                                   {
                                       _overallProductionModeState = aResponseReceivedArgs.Response.OverallResult;
                                       _ecuProductionModeStateList.Clear();
                                       foreach (var state in aResponseReceivedArgs.Response.CurrentProductionModeState.OrderBy((aProductionModeState)=> aProductionModeState.BaudAddress))
                                       {
                                           _ecuProductionModeStateList.Add(new EcuState(state));
                                       }
                                   });
            DisplayActionInfo(Resources.ACTION_INFO_READING_COMPLETE,ActionInfoLogLevel.Info);
            ResponseReceivedEvent?.Invoke(this, null);
            UnlockMenuBar();
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="CommandToolGotFocus"/> empfangen wird. 
        /// </summary>
        /// <param name="aSender">Nicht benötigt</param>
        /// <param name="aCommandToolGotFocus">ToolGetFocus Command-Message</param>
        private void OnToolGotFocusReceived(object aSender, CommandToolGotFocus aCommandToolGotFocus)
        {
            FocusReceived?.Invoke(aSender, aCommandToolGotFocus);
        }
    }
}
