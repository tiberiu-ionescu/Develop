// Copyright (c) AUDI AG 2021
// 
// Autor: Höpp, Michael (I/EE-87, extern)
// Erstellt: 16.11.2020

namespace SpecialTools.ProductionModeService.Messaging.ActionInfo
{
    using System;
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using JetBrains.Annotations;

    /// <summary>
    /// Message Klasse um die Action Info zu verarbeiten
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class CommandSendActionInfo : IMessage
    {
        /// <summary>
        /// Loglevel zum formatieren der Ausgabe
        /// </summary>
        public ActionInfoLogLevel LogLevel { get; set; } = ActionInfoLogLevel.Unknown;

        /// <summary>
        /// Nachricht die in der Action Info ausgegeben wird
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Deserialisierungskonstruktor
        /// </summary>
        [Obsolete("Only for deserialization", true)]
        public CommandSendActionInfo()
        {

        }
        
        /// <summary>
        /// Erstellt ein Command zum Darstellen einer Action Info Nachricht
        /// </summary>
        /// <param name="aMessage">Nachricht</param>
        /// <param name="aLogLevel">Log Level</param>
        public CommandSendActionInfo(string aMessage, ActionInfoLogLevel aLogLevel)
        {
            LogLevel = aLogLevel;
            Message = aMessage;
        }
    }
}
