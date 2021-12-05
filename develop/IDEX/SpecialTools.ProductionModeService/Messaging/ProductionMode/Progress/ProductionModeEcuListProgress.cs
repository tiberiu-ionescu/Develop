// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Progress
{
    using System;
    using System.Collections.Generic;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;
    using Types;

    /// <summary>
    /// Fortschrittsnachricht für die Teilnehmer am Production Mode
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class ProductionModeEcuListProgress:IResponse
    {
        ///<inheritdoc/>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Liste mit ECUs deren Zustand zum Zeitpunkt dieses Fortschritts noch nicht fest steht
        /// </summary>
        public List<EcuProductionModeState> CurrentProductionModeState { get; set; }
    }
}
