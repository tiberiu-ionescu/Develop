// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Requests
{
    using System;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;

    /// <summary>
    /// Liest den Produktionsmodus
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class ReadProductionModeQuery : IRequest
    {
        ///<inheritdoc/>
        public Guid RequestId { get; set; }
        
        ///<inheritdoc/>
        public MessageSpace ResponseSpace { get; set; }
    }
}
