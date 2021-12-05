// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 02.03.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Responses
{
    using System;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;

    /// <summary>
    /// Ergebnis für den Fall, dass das Gateway die Anfrage ablehnt, weil es ausgelastet ist
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class GatewayBusyResult: IResponse
    {
        /// <summary>
        /// Anfrage ID
        /// </summary>
        public Guid RequestId { get; set; }
    }
}
