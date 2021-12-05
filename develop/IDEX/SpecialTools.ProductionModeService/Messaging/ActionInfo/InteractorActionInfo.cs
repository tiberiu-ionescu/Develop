// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EG-14, extern)
// Erstellt: 08.11.2021

namespace SpecialTools.ProductionModeService.Messaging.ActionInfo
{
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using IdexCommonCommunication.IdexMessages;

    /// <summary>
    /// Action Info Interactor
    /// </summary>
    internal sealed class InteractorActionInfo
    {
        /// <summary>
        /// Communicator über den die Messages verarbeitet werden
        /// </summary>
        private readonly IInterprocessCommunicator _communicator;

        /// <summary>
        /// Konstruktor übernimmt die benötigten Referenzen
        /// </summary>
        /// <param name="aCommunicator">Referenz auf den Communicator über den die Messages verarbeitet werden </param>
        internal InteractorActionInfo(IInterprocessCommunicator aCommunicator)
        {
            _communicator = aCommunicator;

            RegisterMessages();
        }
        
        /// <summary>
        /// Registriert die Messages die versendet werden sollen
        /// </summary>
        private void RegisterMessages()
        {
            _communicator.AddToMessageRegistry(typeof(CommandSendActionInfo),true);
        }
        
        /// <summary>
        /// Sendet eine Nachricht an die Action Info
        /// </summary>
        internal void SendActionInfo(string aActionInfoMessage, ActionInfoLogLevel aLogLevel)
        {
            _communicator.SendMessage(new CommandSendActionInfo(aActionInfoMessage, aLogLevel), MessagingConfiguration.DefaultMessageSpaceFor<CommandSendActionInfo>());
        }
    }
}
