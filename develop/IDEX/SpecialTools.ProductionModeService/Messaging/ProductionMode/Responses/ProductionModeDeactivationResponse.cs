// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Responses
{
    using System;
    using System.Collections.Generic;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;
    using Types;

    /// <summary>
    /// Ergebnis des Deaktivierens des Produktionsmodus
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class ProductionModeDeactivationResponse : IResponse
    {
        ///<inheritdoc/>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Jetztiger Produktionsmodus Zustand mit einzelnen Informationen zu allen Steuergeräten
        /// </summary>
        public List<EcuProductionModeState> CurrentProductionModeState { get; set; }

        /// <summary>
        /// Gesamtstatus des Produktionsmodus
        /// </summary>
        public ProductionModeState OverallResult { get; set; }
        
        /// <summary>
        /// Erstellt ein Ergebnis zum Deaktivieren des Produktionsmodus
        /// </summary>
        public ProductionModeDeactivationResponse(List<EcuProductionModeState> aCurrentStatus, ProductionModeState aOverallStatus)
        {
            CurrentProductionModeState = aCurrentStatus;
            OverallResult = aOverallStatus;
        }
        
        /// <summary>
        /// Deserialisierungskonstruktor
        /// </summary>
        [Obsolete("Only for deserialization", true)]
        public ProductionModeDeactivationResponse()
        {
        }
    }
}
