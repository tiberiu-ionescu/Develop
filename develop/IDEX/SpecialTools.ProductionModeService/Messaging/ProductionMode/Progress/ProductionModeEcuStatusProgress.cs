// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Progress
{
    using System;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;
    using Types;

    /// <summary>
    /// Fortschrittsnachricht für die Teilnehmer am Production Mode
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class ProductionModeEcuStatusProgress:IResponse
    {
        ///<inheritdoc/>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Geändertes Steuergerät
        /// </summary>
        public EcuProductionModeState ChangedEcu { get; set; }
        
        /// <summary>
        /// Für die Änderung über den P-Mode Master konfiguriert
        /// </summary>
        public bool WaitingForMasterFlip { get; set; }
    }
}
