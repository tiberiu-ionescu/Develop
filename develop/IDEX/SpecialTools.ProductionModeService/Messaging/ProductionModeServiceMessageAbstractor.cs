// Copyright (c) AUDI AG 2020
// 
// Autor: Josovic, Nenad (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging
{
    using System;
    using ActionInfo;
    using De.VwAudi.Common.Util.Communication.Factory;
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using EventArguments;
    using IdexCommonCommunication.IdexMessages;
    using IdexCommonCommunication.Interaction;
    using MenuBar;
    using ProductionMode;
    using ProductionMode.Progress;
    using ProductionMode.Responses;
    using ToolGotFocus;
    using ToolInfo;

    /// <summary>
    /// Klasse zum kommunizieren über das Messaging System
    /// </summary>
    internal sealed class ProductionModeServiceMessageAbstractor
    {
        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den No Project Result empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, NoProjectResult>> NoProjectResultReceived
        {
            add { _interactorProductionMode.NoProjectResultReceived += value; }
            remove { _interactorProductionMode.NoProjectResultReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Production Mode Activation Response empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeActivationResponse>> ProductionModeActivationResponseReceived
        {
            add { _interactorProductionMode.ProductionModeActivationResponseReceived += value; }
            remove { _interactorProductionMode.ProductionModeActivationResponseReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Production Mode Deactivation Response empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeDeactivationResponse>> ProductionModeDeactivationResponseReceived
        {
            add { _interactorProductionMode.ProductionModeDeactivationResponseReceived += value; }
            remove { _interactorProductionMode.ProductionModeDeactivationResponseReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Production Mode Ecu List Progress empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress>> ProductionModeEcuListProgressReceived
        {
            add { _interactorProductionMode.ProductionModeEcuListProgressReceived += value; }
            remove { _interactorProductionMode.ProductionModeEcuListProgressReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Production Mode Ecu Status Progress empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuStatusProgress>> ProductionModeEcuStatusProgressReceived
        {
            add { _interactorProductionMode.ProductionModeEcuStatusProgressReceived += value; }
            remove { _interactorProductionMode.ProductionModeEcuStatusProgressReceived -= value; }
        }
        
        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Production Mode Ecu Status Progress empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuModificationStartedProgress>> ProductionModeEcuModificationStartedProgressReceived
        {
            add { _interactorProductionMode.ProductionModeEcuModificationStartedProgressReceived += value; }
            remove { _interactorProductionMode.ProductionModeEcuModificationStartedProgressReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Production Mode Not Supported Result empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeNotSupportedResult>> ProductionModeNotSupportedResultReceived
        {
            add { _interactorProductionMode.ProductionModeNotSupportedResultReceived += value; }
            remove { _interactorProductionMode.ProductionModeNotSupportedResultReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Read Production Mode Result empfangen wird
        /// </summary>
        public event EventHandler<ResponseReceivedEventArgs<IRequest, ReadProductionModeResult>> ReadProductionModeResultReceived
        {
            add { _interactorProductionMode.ReadProductionModeResultReceived += value; }
            remove { _interactorProductionMode.ReadProductionModeResultReceived -= value; }
        }

        /// <summary>
        /// Event wird ausgelöst, wenn die Antwort mit den Tool Got Focus empfangen wird
        /// </summary>
        public event EventHandler<CommandToolGotFocus> ToolGotFocusReceived
        {
            add { _interactorToolGotFocus.CommandToolGotFocusReceived += value; }
            remove { _interactorToolGotFocus.CommandToolGotFocusReceived -= value; }
        }

        /// <summary>
        /// Referenz auf den SpecialModuleCommunicator
        /// </summary>
        private static ProductionModeServiceMessageAbstractor _productionModeServiceMessageAbstractorSingleton;

        /// <summary>
        /// Communicator über den die Messages verarbeitet werden
        /// </summary>
        private IInterprocessCommunicator _communicator;

        /// <summary>
        /// Referenz auf die <see cref="InteractorProductionMode"/>
        /// </summary>
        private InteractorProductionMode _interactorProductionMode;

        /// <summary>
        /// Referenz auf die <see cref="InteractorToolGotFocus"/>
        /// </summary>
        private InteractorToolGotFocus _interactorToolGotFocus;

        /// <summary>
        /// Interactor zum verschicken von Action Info Nachrichten
        /// </summary>
        private InteractorActionInfo _interactorActionInfo;

        /// <summary>
        /// Interactor zum verschicken von Menu Bar Nachrichten
        /// </summary>
        private InteractorMenuBar _interactorMenuBar;

        /// <summary>
        /// Konstruktor
        /// </summary>
        private ProductionModeServiceMessageAbstractor()
        {
            InitializeAbstractor();
            CreateMessageInteractors();
        }

        /// <summary>
        /// Gibt den SpecialModuleCommunicator als Singleton zurück
        /// </summary>
        /// <returns>SpecialModuleCommunicator Referenz</returns>
        public static ProductionModeServiceMessageAbstractor GetDefaultCommunicator()
        {
            return _productionModeServiceMessageAbstractorSingleton ?? (_productionModeServiceMessageAbstractorSingleton = new ProductionModeServiceMessageAbstractor());
        }

        /// <summary>
        /// Query Read Production Mode
        /// </summary>
        public void QueryReadProductionMode() => _interactorProductionMode.QueryReadProductionMode();

        /// <summary>
        /// Request Activate Production Mode
        /// </summary>
        public void RequestProductionModeActivate() => _interactorProductionMode.RequestProductionModeActivate();

        /// <summary>
        /// Request Deactivate Production Mode
        /// </summary>
        public void RequestProductionModeDeactivate() => _interactorProductionMode.RequestProductionModeDeactivate();

        /// <summary>
        /// Zeigt etwas auf der Action Info an
        /// </summary>
        /// <param name="aActionInfoMessage">Action Info Nachricht</param>
        /// <param name="aLogLevel">Log Level der Action Info</param>
        public void DisplayActionInfo(string aActionInfoMessage, ActionInfoLogLevel aLogLevel) => _interactorActionInfo.SendActionInfo(aActionInfoMessage, aLogLevel);

        /// <summary>
        /// Beendet den Message Abstractor
        /// </summary>
        internal void Shutdown()
        {
            _communicator.Shutdown();
        }

        /// <summary>
        /// Instanziiert die Message Interactors
        /// </summary>
        private void CreateMessageInteractors()
        {
            var unused = new InteractorToolInfo(_communicator);
            _interactorToolGotFocus = new InteractorToolGotFocus(_communicator);
            _interactorProductionMode = new InteractorProductionMode(_communicator);
            _interactorActionInfo = new InteractorActionInfo(_communicator);
            _interactorMenuBar = new InteractorMenuBar(_communicator);
        }

        /// <summary>
        /// Sperrt die Menüleiste
        /// </summary>
        public void LockMenuBar()
        {
            _interactorMenuBar.LockMenuBar();
        }
        
        /// <summary>
        /// Schaltet die Menüleiste wiede frei
        /// </summary>
        public void UnlockMenuBar()
        {
            _interactorMenuBar.UnlockMenuBar();
        }

        /// <summary>
        /// Initialisiert den Message Abstractor
        /// </summary>
        private void InitializeAbstractor()
        {
            var configuration = MessagingConfiguration.DefaultIdexMessagingConfiguration;
            _communicator = InterprocessCommunicatorFactory.CreateInterprocessCommunicator(configuration);

            var localBroker = _communicator.ConnectToLocal();
            localBroker?.ShutdownWhenUnused();
            _communicator.WaitUntilConnected();
        }
    }
}
