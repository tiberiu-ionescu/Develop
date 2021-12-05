// Copyright (c) AUDI AG 2021
// 
// Autor: Höpp, Michael (I/EE-87, extern)
// Erstellt: 16.11.2020

namespace SpecialTools.ProductionModeService.Messaging.ActionInfo
{
    /// <summary>
    /// Enum stellt die Log Level der Action Info Controls dar
    /// </summary>
    public enum ActionInfoLogLevel
    {
        /// <summary>
        /// Default Wert, wenn z.B. der Loglevel nicht in der Message festgelegt ist.
        /// </summary>
        Unknown,

        /// <summary>
        /// Wird benutzt um dem User eine Information anzuzeigen
        /// </summary>
        Info,

        /// <summary>
        /// Wird benutzt um dem User eine Warnung anzuzeigen
        /// </summary>
        Warn,

        /// <summary>
        /// Wird benutzt um dem User das auftreten eines Fehlers anzuzeigen
        /// </summary>
        Error
    }
}
