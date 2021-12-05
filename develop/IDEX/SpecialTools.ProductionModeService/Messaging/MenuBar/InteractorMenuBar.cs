// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EG-14, extern)
// Erstellt: 08.11.2021

namespace SpecialTools.ProductionModeService.Messaging.MenuBar
{
    using ActionInfo;
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using IdexCommonCommunication.IdexMessages;

    /// <summary>
    /// Menüleisten Interactor
    /// </summary>
    public class InteractorMenuBar

    {
        /// <summary>
        /// Communicator über den die Messages verarbeitet werden
        /// </summary>
        private readonly IInterprocessCommunicator _communicator;

        /// <summary>
        /// Konstruktor übernimmt die benötigten Referenzen
        /// </summary>
        /// <param name="aCommunicator">Referenz auf den Communicator über den die Messages verarbeitet werden </param>
        public InteractorMenuBar(IInterprocessCommunicator aCommunicator)
        {
            _communicator = aCommunicator;

            RegisterMessages();
        }

        /// <summary>
        /// Registriert die Messages die versendet werden sollen
        /// </summary>
        private void RegisterMessages()
        {
            _communicator.AddToMessageRegistry(typeof(CommandSendActionInfo), true);
        }

        /// <summary>
        /// Lockt die Menüleiste
        /// </summary>
        internal void LockMenuBar()
        {
            _communicator.SendMessage(new CommandToolWorkStarted(),MessagingConfiguration.DefaultMessageSpaceFor<CommandToolWorkStarted>());
        }

        /// <summary>
        /// Schaltet die Menüleiste wieder frei
        /// </summary>
        internal void UnlockMenuBar()
        {
            _communicator.SendMessage(new CommandToolWorkFinished(),MessagingConfiguration.DefaultMessageSpaceFor<CommandToolWorkFinished>());
        }
    }
}
