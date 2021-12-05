// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Types
{
    /// <summary>
    /// Ergebnistyp des Produktionsmodus
    /// </summary>
    public enum ProductionModeResultType
    {
        /// <summary>
        /// Unbekannter Fehler
        /// </summary>
        UnknownError,

        /// <summary>
        /// Kein Fehler
        /// </summary>
        NoError,

        /// <summary>
        /// Anpassung fehlt
        /// </summary>
        AdaptionMissing,

        /// <summary>
        /// NRC beim Schreiben der Anpassung
        /// </summary>
        AdaptionWriteNrc,

        /// <summary>
        /// Status hat sich entgegen der Erwartungen nicht geändert
        /// </summary>
        StateDidNotChange,

        /// <summary>
        /// Production Mode Master ist nicht verfügbar oder konnte nicht gesetzt werden
        /// </summary>
        PModeMasterNotSet,

        /// <summary>
        /// Diagnostic Task konnte nicht erstellt werden
        /// </summary>
        DiagnosticTaskCreationFailed,

        /// <summary>
        /// Spezieller Hex Service hat einen NRC geworfen
        /// </summary>
        CustomHexServiceNrc,

        /// <summary>
        /// Zugriff Verweigert
        /// </summary>
        AccessDenied,

        /// <summary>
        /// Neustart ist Fehlgeschlagen
        /// </summary>
        RestartFailed,

        /// <summary>
        /// Messwertblock für das Auslesen des Produktionsmodus konnte nicht gefunden werden
        /// </summary>
        MeasurementBlockMissing,

        /// <summary>
        /// Anpassung konnte nicht geschrieben werden
        /// </summary>
        AdaptionWriteFailed,

        /// <summary>
        /// Verbindung zum Logical Link konnte nicht hergestellt werden
        /// </summary>
        ConnectionFailed,

        /// <summary>
        /// Änderung der Session ist Fehlgeschlagen
        /// </summary>
        SessionChangeFailed,

        /// <summary>
        /// Spezieller Hex Service konnte nicht versendet werden
        /// </summary>
        CustomHexServiceDispatchFailed
    }
}
