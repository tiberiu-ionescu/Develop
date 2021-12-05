// Copyright (c) AUDI AG 2020
// 
// Autor: Josovic, Nenad (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ToolGotFocus
{
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using JetBrains.Annotations;

    /// <summary>
    /// Message Klasse sendet das ToolGetFocus Command-Message
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class CommandToolGotFocus : IMessage
    {
    }
}
