// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 21.10.2020

namespace SpecialTools.ProductionModeService.Messaging.ToolGotFocus
{
    using System;
    using De.VwAudi.Common.Util.Communication.Events;
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using IdexCommonCommunication.IdexMessages;

    /// <summary>
    /// Klasse verarbeitet die ToolGotFocus Funktion
    /// </summary>
    internal sealed class InteractorToolGotFocus
    {
        /// <summary>
        /// Event wird ausgelöst, wenn eine Action Info empfangen wird
        /// </summary>
        public event EventHandler<CommandToolGotFocus> CommandToolGotFocusReceived;

        /// <summary>
        /// Communicator über den die Messages verarbeitet werden
        /// </summary>
        private readonly IInterprocessCommunicator _communicator;

        /// <summary>
        /// Konstruktor übernimmt die benötigten Referenzen
        /// </summary>
        /// <param name="aCommunicator">Referenz auf den Communicator über den die Messages verarbeitet werden </param>
        public InteractorToolGotFocus(IInterprocessCommunicator aCommunicator)
        {
            _communicator = aCommunicator;

            RegisterMessages();
        }

        /// <summary>
        /// Wird ausgelöst wenn die <see cref="CommandToolGotFocus"/> Nachricht empfangen wird
        /// </summary>
        /// <param name="aEventArgs">Argumente mit der Nachricht</param>
        private void OnCommandToolGotFocusReceived(TypedMessageReceivedEventArgs<CommandToolGotFocus> aEventArgs)
        {
            CommandToolGotFocusReceived?.Invoke(this, aEventArgs.TypedMessage);
        }

        /// <summary>
        /// Registriert die Messages die empfangen werden sollen.
        /// </summary>
        private void RegisterMessages()
        {
            _communicator.Register<CommandToolGotFocus>(MessagingConfiguration.DefaultMessageSpaceFor<CommandToolGotFocus>(), OnCommandToolGotFocusReceived, false).WaitUntilSubscriptionConfirmed();
        }
    }
}
