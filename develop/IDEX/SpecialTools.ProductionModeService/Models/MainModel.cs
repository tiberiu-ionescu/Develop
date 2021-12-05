// Copyright (c) AUDI AG 2020
// 
// Autor: Josovic, Nenad (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Models
{
    using System;
    using EventArguments;
    using IdexCommonCommunication.Interaction;
    using Messaging;
    using Messaging.ActionInfo;
    using Messaging.ProductionMode.Progress;
    using Messaging.ProductionMode.Responses;
    using Messaging.ToolGotFocus;

    /// <summary>
    /// Model zu dem MainViewModel
    /// </summary>
    internal sealed class MainModel
    {
        /// <summary>
        /// Event wird ausgelöst, wenn eine No Project Result empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, NoProjectResult>> NoProjectResultReceived
        {
            add { _messageCommunicator.NoProjectResultReceived += value; }
            remove { _messageCommunicator.NoProjectResultReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Activated Response empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeActivationResponse>> ProductionModeActivationResponseReceived
        {
            add { _messageCommunicator.ProductionModeActivationResponseReceived += value; }
            remove { _messageCommunicator.ProductionModeActivationResponseReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Deactivated Response empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeDeactivationResponse>> ProductionModeDeactivationResponseReceived
        {
            add { _messageCommunicator.ProductionModeDeactivationResponseReceived += value; }
            remove { _messageCommunicator.ProductionModeDeactivationResponseReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Ecu List Progress empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress>> ProductionModeEcuListProgressReceived
        {
            add { _messageCommunicator.ProductionModeEcuListProgressReceived += value; }
            remove { _messageCommunicator.ProductionModeEcuListProgressReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Ecu Status Progress empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuStatusProgress>> ProductionModeEcuStatusProgressReceived
        {
            add { _messageCommunicator.ProductionModeEcuStatusProgressReceived += value; }
            remove { _messageCommunicator.ProductionModeEcuStatusProgressReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Production Mode Ecu Status Progress empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuModificationStartedProgress>> ProductionModeEcuModificationStartedProgressReceived
        {
            add { _messageCommunicator.ProductionModeEcuModificationStartedProgressReceived += value; }
            remove { _messageCommunicator.ProductionModeEcuModificationStartedProgressReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Not Supported Result empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeNotSupportedResult>> ProductionModeNotSupportedResultReceived
        {
            add { _messageCommunicator.ProductionModeNotSupportedResultReceived += value; }
            remove { _messageCommunicator.ProductionModeNotSupportedResultReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn eine Read Production Mode Result empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ReadProductionModeResult>> ReadProductionModeResultReceived
        {
            add { _messageCommunicator.ReadProductionModeResultReceived += value; }
            remove { _messageCommunicator.ReadProductionModeResultReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Tool Got Focus empfangen wird
        /// </summary>
        internal event EventHandler<CommandToolGotFocus> ToolGotFocusReceived
        {
            add { _messageCommunicator.ToolGotFocusReceived += value; }
            remove { _messageCommunicator.ToolGotFocusReceived -= value; }
        }

        /// <summary>
        /// Referenz auf den Communicator der das Messaging verwaltet
        /// </summary>
        private ProductionModeServiceMessageAbstractor _messageCommunicator;

        /// <summary>
        /// Konstruktor
        /// </summary>
        internal MainModel()
        {
            InitializeCommunicator();
        }

        /// <summary>
        /// Query Read Production Mode
        /// </summary>
        public void QueryReadProductionMode() => _messageCommunicator.QueryReadProductionMode();

        /// <summary>
        /// Request Activate Production Mode
        /// </summary>
        public void RequestProductionModeActivate() => _messageCommunicator.RequestProductionModeActivate();

        /// <summary>
        /// Request Deactivate Production Mode
        /// </summary>
        public void RequestProductionModeDeactivate() => _messageCommunicator.RequestProductionModeDeactivate();

        /// <summary>
        /// Zeigt etwas auf der Action Info an
        /// </summary>
        /// <param name="aActionInfoMessage">Action Info Nachricht</param>
        /// <param name="aLogLevel">Log Level der Action Info</param>
        public void DisplayActionInfo(string aActionInfoMessage, ActionInfoLogLevel aLogLevel) => _messageCommunicator.DisplayActionInfo(aActionInfoMessage, aLogLevel);

        /// <summary>
        /// Sperrt die Menüleiste
        /// </summary>
        public void LockMenuBar() => _messageCommunicator.LockMenuBar();

        /// <summary>
        /// Schaltet die Menüleiste wiede frei
        /// </summary>
        public void UnlockMenuBar() => _messageCommunicator.UnlockMenuBar();

        /// <summary>
        /// Konfiguriert den Communicator um auf bestimmte Nachrichten zu reagieren
        /// </summary>
        private void InitializeCommunicator()
        {
            _messageCommunicator = ProductionModeServiceMessageAbstractor.GetDefaultCommunicator();
        }
    }
}
