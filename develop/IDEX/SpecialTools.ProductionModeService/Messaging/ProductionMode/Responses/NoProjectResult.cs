// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Responses
{
    using System;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;

    /// <summary>
    /// Ergebnis für den Fall, dass es kein Projekt gibt
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public class NoProjectResult : IResponse
    {
        /// <summary>
        /// Anfrage ID
        /// </summary>
        public Guid RequestId { get; set; }
    }
}
