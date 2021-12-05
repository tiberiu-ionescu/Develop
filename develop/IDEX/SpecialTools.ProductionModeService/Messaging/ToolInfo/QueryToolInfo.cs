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
    /// Message Klasse sendet die QueryGetToolInfo Message
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class QueryToolInfo : IMessage
    {
    }
}
