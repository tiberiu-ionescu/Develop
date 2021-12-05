// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Types
{
    /// <summary>
    /// Produktionsmodus Zustand
    /// </summary>
    public enum ProductionModeState
    {
        /// <summary>
        /// Ungültiger oder Unbekannter Zustand
        /// </summary>
        InvalidOrUnknown = 0,
        
        /// <summary>
        /// Aktiv
        /// </summary>
        Active,
        
        /// <summary>
        /// Inaktiv
        /// </summary>
        Inactive,
    }
}
