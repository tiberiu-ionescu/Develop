//  * Copyright (c) AUDI AG 2017
//  *
//  * Autor: Funk, Jan (I/EE-45)
//  * Erstellt: 21.02.2017
//  */

namespace MeasurementsModule.Enum
{
    /// <summary>
    /// Status zum Steuern der UserControls
    /// </summary>
    internal enum PanelControlStates
    {
        /// <summary>
        /// Springt zur in die SG Funktionen
        /// </summary>
        SwitchBack = 0,

        /// <summary>
        /// wechsel zum nächsten Panel
        /// </summary>
        Next = 1,

        /// <summary>
        /// Lädt das Haupt Panel
        /// </summary>
        MainPanel = 2,

        /// <summary>
        /// Druckt das Protokoll aus
        /// </summary>
        PrintProtocol = 6,

        /// <summary>
        /// Speichert die Messwerte in der VehicleData
        /// </summary>
        SaveInVehicleData = 7,

        /// <summary>
        /// Liest die verfügbaren Messwerte aus.
        /// </summary>
        ReadAvailableMeasurements
    }
}
