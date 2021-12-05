// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 05.11.2020

namespace SpecialTools.ProductionModeService.Messaging.ToolInfo
{
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using JetBrains.Annotations;

    /// <summary>
    /// Message Klasse für die Antwort auf die QueryGetToolInfo Message
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class ResultDisplayToolDescription : IMessage
    {
        /// <summary>
        /// Wird genutzt um dem Anwender eine detailiertere Beschreibung des Tools zu liefern. 
        /// Kann bei Bedarf auch durch das Tool selbst lokalisiert ausgegeben werden.
        /// </summary>
        public string DisplayToolDescription { get; set; }

        /// <summary>
        /// Id des Tool 
        /// </summary>
        public string Id { set; get; }
    }
}
