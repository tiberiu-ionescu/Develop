// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Types
{
    using JetBrains.Annotations;

    /// <summary>
    /// Produktionsmodus Status eines Steuergeräts, Inhalt einiger Messages
    /// </summary>
    [PublicAPI]
    public class EcuProductionModeState
    {
        /// <summary>
        /// Baud Adresse
        /// </summary>
        public uint BaudAddress { get; set; }

        /// <summary>
        /// Übersetzter Steuergerät Name
        /// </summary>
        public string LocalizedEcuName { get; set; }
        
        /// <summary>
        /// Antwort Typ
        /// </summary>
        public ProductionModeResultType ResultType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public ProductionModeState State { get; set; }
    }
}