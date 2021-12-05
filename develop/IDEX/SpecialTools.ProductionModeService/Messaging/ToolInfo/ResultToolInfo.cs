// Copyright (c) AUDI AG 2020
// 
// Autor: Klein, Dimitri (I/EE-87, extern)
// Erstellt: 14.10.2020

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
    public sealed class ResultToolInfo : IMessage
    {
        /// <summary>
        /// Id des Tool 
        /// </summary>
        public string Id { set; get; } = string.Empty;

        /// <summary>
        /// Einfacher Name des Tools der zB fürs Logging genutzt wird. 
        /// Falls möglich sollten nur Ascii Zeichen genutzt werden.
        /// </summary>
        public string InternalToolName { get; set; } = string.Empty;
    }
}
