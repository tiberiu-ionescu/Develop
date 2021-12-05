// Copyright (c) AUDI AG 2020
// 
// Autor: Josovic, Nenad (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode
{
    using System;
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using EventArguments;
    using IdexCommonCommunication.IdexMessages;
    using IdexCommonCommunication.Interaction;
    using NLog;
    using Progress;
    using Requests;
    using Responses;

    /// <summary>
    /// Nachrichteninteractor Transport Mode
    /// </summary>
    internal sealed class InteractorProductionMode
    {
        /// <summary>
        /// Event wird ausgelöst, wenn eine No Project Result empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, NoProjectResult>> NoProjectResultReceived;

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Activation Response empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeActivationResponse>> ProductionModeActivationResponseReceived;

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Deactivation Response empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeDeactivationResponse>> ProductionModeDeactivationResponseReceived;

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Ecu List Progress empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress>> ProductionModeEcuListProgressReceived;

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Ecu Status Progress empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuStatusProgress>> ProductionModeEcuStatusProgressReceived;
        
        /// <summary>
        /// Event wird ausgelöst, wenn begonnen wird den Produktionsmodus eines Production Mode Ecus zu modifizieren 
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeEcuModificationStartedProgress>> ProductionModeEcuModificationStartedProgressReceived;

        /// <summary>
        /// Event wird ausgelöst, wenn eine Production Mode Not Supported Result empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ProductionModeNotSupportedResult>> ProductionModeNotSupportedResultReceived;

        /// <summary>
        /// Event wird ausgelöst, wenn eine Read Production Mode Result Response empfangen wird
        /// </summary>
        internal event EventHandler<ResponseReceivedEventArgs<IRequest, ReadProductionModeResult>> ReadProductionModeResultReceived;

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Communicator über den die Messages verarbeitet werden
        /// </summary>
        private readonly InteractorClientCommunicator _communicator;

        /// <summary>
        /// Im Interactor registrierter Request für das Versenden eines Production Mode Activate
        /// </summary>
        private RegisteredRequest<ProductionModeActivateRequest> _productionModeActivateRequestRegistration;

        /// <summary>
        /// Im Interactor registrierter Request für das Versenden eines Production Mode Deactivate
        /// </summary>
        private RegisteredRequest<ProductionModeDeactivateRequest> _productionModeDeactivateRequestRegistration;

        /// <summary>
        /// Im Interactor registrierter Request für das Versenden eines Read Production Mode Query
        /// </summary>
        private RegisteredRequest<ReadProductionModeQuery> _readProductionModeQueryRegistration;

        /// <summary>
        /// Konstruktor übernimmt die benötigten Referenzen
        /// </summary>
        /// <param name="aCommunicator">Referenz auf den Communicator über den die Messages verarbeitet werden </param>
        public InteractorProductionMode(IInterprocessCommunicator aCommunicator)
        {
            _communicator = new InteractorClientCommunicator(aCommunicator);

            RegisterMessages();
        }

        /// <summary>
        /// Senden den Request für Activate Production Mode
        /// </summary>
        internal ReadProductionModeQuery QueryReadProductionMode()
        {
            var originalRequest = new ReadProductionModeQuery();
            _readProductionModeQueryRegistration.Send(originalRequest);
            return originalRequest;
        }

        /// <summary>
        /// Senden den Request für Activate Production Mode
        /// </summary>
        internal ProductionModeActivateRequest RequestProductionModeActivate()
        {
            var originalRequest = new ProductionModeActivateRequest();
            _productionModeActivateRequestRegistration.Send(originalRequest);
            return originalRequest;
        }

        /// <summary>
        /// Senden den Request für Activate Production Mode
        /// </summary>
        internal ProductionModeDeactivateRequest RequestProductionModeDeactivate()
        {
            var originalRequest = new ProductionModeDeactivateRequest();
            _productionModeDeactivateRequestRegistration.Send(originalRequest);
            return originalRequest;
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="GatewayBusyResult"/> empfangen wird.
        /// </summary>
        /// <param name="aResponse">Ergebnis des Lesens des Produktionsmodus</param>
        private void OnGatewayBusyResultReceived(ResponseReceivedEventArgs<IRequest, GatewayBusyResult> aResponse)
        {
            LOGGER.Warn($"Request has been denied by the gateway since it's busy. Request Id: {aResponse.Response.RequestId.ToString()}");
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="NoProjectResult"/> empfangen wird. 
        /// </summary>
        /// <param name="aResponse">Antwort dafür, dass kein Projekt gefunden wurde</param>
        private void OnNoProjectResultReceived(ResponseReceivedEventArgs<IRequest, NoProjectResult> aResponse)
        {
            NoProjectResultReceived?.Invoke(this, aResponse);
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="ProductionModeActivationResponse"/> empfangen wird.
        /// </summary>
        /// <param name="aResponse">Ergebnis des Aktivierens des Produktionsmodus</param>
        private void OnProductionModeActivationResponseReceived(ResponseReceivedEventArgs<IRequest, ProductionModeActivationResponse> aResponse)
        {
            ProductionModeActivationResponseReceived?.Invoke(this, aResponse);
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="ProductionModeActivationResponse"/> empfangen wird.
        /// </summary>
        /// <param name="aResponse">Ergebnis des Deaktivierens des Produktionsmodus</param>
        private void OnProductionModeDeactivationResponseReceived(ResponseReceivedEventArgs<IRequest, ProductionModeDeactivationResponse> aResponse)
        {
            ProductionModeDeactivationResponseReceived?.Invoke(this, aResponse);
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="ProductionModeEcuListProgress"/> empfangen wird.
        /// </summary>
        /// <param name="aResponse">Ergebnis des Lesens des Produktionsmodus</param>
        private void OnProductionModeEcuListProgressReceived(ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress> aResponse)
        {
            ProductionModeEcuListProgressReceived?.Invoke(this, aResponse);
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="ProductionModeEcuStatusProgress"/> empfangen wird.
        /// </summary>
        /// <param name="aResponse">Ergebnis des Lesens des Produktionsmodus</param>
        private void OnProductionModeEcuStatusProgressReceived(ResponseReceivedEventArgs<IRequest, ProductionModeEcuStatusProgress> aResponse)
        {
            ProductionModeEcuStatusProgressReceived?.Invoke(this, aResponse);
        }
        
        /// <summary>
        /// Wird ausgeführt, wenn eine <see cref="ProductionModeEcuModificationStartedProgress"/> Nachricht empfangen wird.
        /// </summary>
        /// <param name="aResponse">Ergebnis des Lesens des Produktionsmodus</param>
        private void OnProductionModeEcuModificationStartedProgressReceived(ResponseReceivedEventArgs<IRequest, ProductionModeEcuModificationStartedProgress> aResponse)
        {
            ProductionModeEcuModificationStartedProgressReceived?.Invoke(this, aResponse);
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="ProductionModeNotSupportedResult"/> empfangen wird. 
        /// </summary>
        /// <param name="aResponse">Antwort dafür, dass kein Projekt gefunden wurde</param>
        private void OnProductionModeNotSupportedResultReceived(ResponseReceivedEventArgs<IRequest, ProductionModeNotSupportedResult> aResponse)
        {
            ProductionModeNotSupportedResultReceived?.Invoke(this, aResponse);
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="ReadProductionModeResult"/> empfangen wird.
        /// </summary>
        /// <param name="aResponse">Ergebnis des Lesens des Produktionsmodus</param>
        private void OnReadProductionModeResultReceived(ResponseReceivedEventArgs<IRequest, ReadProductionModeResult> aResponse)
        {
            ReadProductionModeResultReceived?.Invoke(this, aResponse);
        }

        /// <summary>
        /// Registriert die Production Mode Activate Messages die empfangen werden sollen.
        /// </summary>
        private void RegisterAndHandleProductionModeActivateMessages()
        {
            _productionModeActivateRequestRegistration = _communicator.RegisterRequest<ProductionModeActivateRequest>(MessagingConfiguration.DefaultMessageSpaceFor<ProductionModeActivateRequest>());
            _productionModeActivateRequestRegistration.HandleResponse<ProductionModeActivationResponse>((aRequest, aResponse)
                                                                                                            => OnProductionModeActivationResponseReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeActivationResponse
                                                                                                                                                          >(aRequest, aResponse)));
            _productionModeActivateRequestRegistration.HandleResponse<NoProjectResult>((aRequest, aResponse) => OnNoProjectResultReceived(new ResponseReceivedEventArgs<IRequest, NoProjectResult>(aRequest, aResponse)));
            _productionModeActivateRequestRegistration.HandleResponse<ProductionModeNotSupportedResult>((aRequest, aResponse)
                                                                                                            => OnProductionModeNotSupportedResultReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeNotSupportedResult
                                                                                                                                                          >(aRequest, aResponse)));
            _productionModeActivateRequestRegistration.HandleResponse<GatewayBusyResult>((aRequest, aResponse) => OnGatewayBusyResultReceived(new ResponseReceivedEventArgs<IRequest, GatewayBusyResult>(aRequest, aResponse)));
            _productionModeActivateRequestRegistration.HandleProgress<ProductionModeEcuListProgress>((aRequest, aResponse)
                                                                                                         => OnProductionModeEcuListProgressReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress>(aRequest, aResponse)));
            _productionModeActivateRequestRegistration.HandleProgress<ProductionModeEcuStatusProgress>((aRequest, aResponse)
                                                                                                           => OnProductionModeEcuStatusProgressReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeEcuStatusProgress>(aRequest, aResponse)));
            _productionModeActivateRequestRegistration.HandleProgress<ProductionModeEcuModificationStartedProgress>((aRequest, aResponse)
                                                                                                           => OnProductionModeEcuModificationStartedProgressReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeEcuModificationStartedProgress>(aRequest, aResponse)));
            _productionModeActivateRequestRegistration.HandleUnknownResponse((aQuery, aResponse) => LOGGER.Error($"Unknown Response of Type {aResponse.MessageName} to Activate Production Mode Request received!"));
        }

        /// <summary>
        /// Registriert die Production Mode Deactivate Messages die empfangen werden sollen.
        /// </summary>
        private void RegisterAndHandleProductionModeDeactivateMessages()
        {
            _productionModeDeactivateRequestRegistration = _communicator.RegisterRequest<ProductionModeDeactivateRequest>(MessagingConfiguration.DefaultMessageSpaceFor<ProductionModeDeactivateRequest>());
            _productionModeDeactivateRequestRegistration.HandleResponse<ProductionModeDeactivationResponse>((aRequest, aResponse)
                                                                                                                => OnProductionModeDeactivationResponseReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeDeactivationResponse>(aRequest,
                                                                                                                                                                                                                                            aResponse)));
            _productionModeDeactivateRequestRegistration.HandleResponse<NoProjectResult>((aRequest, aResponse) => OnNoProjectResultReceived(new ResponseReceivedEventArgs<IRequest, NoProjectResult>(aRequest, aResponse)));
            _productionModeDeactivateRequestRegistration.HandleResponse<GatewayBusyResult>((aRequest, aResponse) => OnGatewayBusyResultReceived(new ResponseReceivedEventArgs<IRequest, GatewayBusyResult>(aRequest, aResponse)));
            _productionModeDeactivateRequestRegistration.HandleProgress<ProductionModeEcuListProgress>((aRequest, aResponse)
                                                                                                           => OnProductionModeEcuListProgressReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress>(aRequest, aResponse)));
            _productionModeDeactivateRequestRegistration.HandleProgress<ProductionModeEcuStatusProgress>((aRequest, aResponse)
                                                                                                             => OnProductionModeEcuStatusProgressReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeEcuStatusProgress>(aRequest, aResponse)));
            _productionModeDeactivateRequestRegistration.HandleProgress<ProductionModeEcuModificationStartedProgress>((aRequest, aResponse)
                                                                                                             => OnProductionModeEcuModificationStartedProgressReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeEcuModificationStartedProgress>(aRequest, aResponse)));
            _productionModeDeactivateRequestRegistration.HandleUnknownResponse((aQuery, aResponse) => LOGGER.Error($"Unknown Response of Type {aResponse.MessageName} to Deactivate Production Mode Request received!"));
        }

        /// <summary>
        /// Registriert die Production Mode Query Messages die empfangen werden sollen.
        /// </summary>
        private void RegisterAndHandleProductionModeQueryMessages()
        {
            _readProductionModeQueryRegistration = _communicator.RegisterRequest<ReadProductionModeQuery>(MessagingConfiguration.DefaultMessageSpaceFor<ReadProductionModeQuery>());
            _readProductionModeQueryRegistration.HandleResponse<ReadProductionModeResult>((aRequest, aResponse) => OnReadProductionModeResultReceived(new ResponseReceivedEventArgs<IRequest, ReadProductionModeResult>(aRequest, aResponse)));
            _readProductionModeQueryRegistration.HandleResponse<NoProjectResult>((aRequest, aResponse) => OnNoProjectResultReceived(new ResponseReceivedEventArgs<IRequest, NoProjectResult>(aRequest, aResponse)));
            _readProductionModeQueryRegistration.HandleResponse<GatewayBusyResult>((aRequest, aResponse) => OnGatewayBusyResultReceived(new ResponseReceivedEventArgs<IRequest, GatewayBusyResult>(aRequest, aResponse)));
            _readProductionModeQueryRegistration.HandleProgress<ProductionModeEcuListProgress>((aRequest, aResponse)
                                                                                                   => OnProductionModeEcuListProgressReceived(new ResponseReceivedEventArgs<IRequest, ProductionModeEcuListProgress>(aRequest, aResponse)));
            _readProductionModeQueryRegistration.HandleUnknownResponse((aQuery, aResponse) => LOGGER.Error($"Unknown Response of Type {aResponse.MessageName} to Read Production Mode Query received!"));
        }

        /// <summary>
        /// Registriert die Messages die empfangen werden sollen.
        /// </summary>
        private void RegisterMessages()
        {
            RegisterAndHandleProductionModeActivateMessages();
            RegisterAndHandleProductionModeDeactivateMessages();
            RegisterAndHandleProductionModeQueryMessages();
        }
    }
}
