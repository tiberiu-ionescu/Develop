// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 21.10.2020

namespace SpecialTools.ProductionModeService.Messaging.ToolInfo
{
    using De.VwAudi.Common.Util.Communication.Events;
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using De.VwAudi.Common.Util.Communication.MessageBroking;
    using IdexCommonCommunication.IdexMessages;

    /// <summary>
    /// Klasse verarbeitet die GetToolInfo Anfrage
    /// </summary>
    internal sealed class InteractorToolInfo
    {
        /// <summary>
        /// Communicator über den die Messages verarbeitet werden
        /// </summary>
        private readonly IInterprocessCommunicator _communicator;

        /// <summary>
        /// Sender Objekt für die <see cref="ResultBitmapIcon"/> Message
        /// </summary>
        private MessageSender<ResultBitmapIcon> _resultBitmapIconSender;

        /// <summary>
        /// Sender Objekt für die <see cref="ResultDisplayGroupName"/> Message
        /// </summary>
        private MessageSender<ResultDisplayGroupName> _resultDisplayGroupNameSender;

        /// <summary>
        /// Sender Objekt für die <see cref="ResultDisplayToolDescription"/> Message
        /// </summary>
        private MessageSender<ResultDisplayToolDescription> _resultDisplayToolDescriptionSender;

        /// <summary>
        /// Sender Objekt für die <see cref="ResultDisplayToolName"/> Message
        /// </summary>
        private MessageSender<ResultDisplayToolName> _resultDisplayToolNameSender;

        /// <summary>
        /// Sender Objekt für die <see cref="ResultSvgIcon"/> Message
        /// </summary>
        private MessageSender<ResultSvgIcon> _resultSvgIconSender;

        /// <summary>
        /// Sender Objekt für die <see cref="ResultToolInfo"/> Message
        /// </summary>
        private MessageSender<ResultToolInfo> _resultToolInfoSender;

        /// <summary>
        /// Konstruktor übernimmt die benötigten Referenzen
        /// </summary>
        /// <param name="aCommunicator">Referenz auf den Communicator über den die Messages verarbeitet werden </param>
        public InteractorToolInfo(IInterprocessCommunicator aCommunicator)
        {
            _communicator = aCommunicator;

            RegisterMessages();
        }

        /// <summary>
        /// Wird ausgeführt, wenn das <see cref="QueryToolInfo"/> empfangen wird. Sendet eine Antwort mit den Tool Informationen zurück
        /// </summary>
        /// <param name="aEventArgs">Nicht benötigt</param>
        private void OnQueryGetToolInfoReceived(TypedMessageReceivedEventArgs<QueryToolInfo> aEventArgs)
        {
            _resultToolInfoSender.SendMessage(new ResultToolInfo
                                              {
                                                  Id = ToolInformation.Id,
                                                  InternalToolName = ToolInformation.ToolName
                                              });
            _resultBitmapIconSender.SendMessage(new ResultBitmapIcon
                                                {
                                                    Id = ToolInformation.Id,
                                                    IconBitmap = ToolInformation.ImageBitmap
                                                });
            _resultSvgIconSender.SendMessage(new ResultSvgIcon
                                             {
                                                 Id = ToolInformation.Id,
                                                 IconSvg = ToolInformation.ImageSvg
                                             });
            _resultDisplayGroupNameSender.SendMessage(new ResultDisplayGroupName
                                                      {
                                                          Id = ToolInformation.Id,
                                                          DisplayGroupName = ToolInformation.LocalizedGroupName
                                                      });
            _resultDisplayToolDescriptionSender.SendMessage(new ResultDisplayToolDescription
                                                            {
                                                                Id = ToolInformation.Id,
                                                                DisplayToolDescription = ToolInformation.LocalizedToolDescription
                                                            });
            _resultDisplayToolNameSender.SendMessage(new ResultDisplayToolName
                                                     {
                                                         Id = ToolInformation.Id,
                                                         DisplayToolName = ToolInformation.LocalizedToolName
                                                     });
        }

        /// <summary>
        /// Registriert die Messages die empfangen werden sollen.
        /// </summary>
        private void RegisterMessages()
        {
            _communicator.Register<QueryToolInfo>(MessagingConfiguration.DefaultMessageSpaceFor<QueryToolInfo>(), OnQueryGetToolInfoReceived, false).WaitUntilSubscriptionConfirmed();

            _resultToolInfoSender = new MessageSender<ResultToolInfo>(_communicator, MessagingConfiguration.DefaultMessageSpaceFor<ResultToolInfo>());
            _resultBitmapIconSender = new MessageSender<ResultBitmapIcon>(_communicator, MessagingConfiguration.DefaultMessageSpaceFor<ResultBitmapIcon>());
            _resultSvgIconSender = new MessageSender<ResultSvgIcon>(_communicator, MessagingConfiguration.DefaultMessageSpaceFor<ResultSvgIcon>());
            _resultDisplayGroupNameSender = new MessageSender<ResultDisplayGroupName>(_communicator, MessagingConfiguration.DefaultMessageSpaceFor<ResultDisplayGroupName>());
            _resultDisplayToolDescriptionSender = new MessageSender<ResultDisplayToolDescription>(_communicator, MessagingConfiguration.DefaultMessageSpaceFor<ResultDisplayToolDescription>());
            _resultDisplayToolNameSender = new MessageSender<ResultDisplayToolName>(_communicator, MessagingConfiguration.DefaultMessageSpaceFor<ResultDisplayToolName>());
        }
    }
}
