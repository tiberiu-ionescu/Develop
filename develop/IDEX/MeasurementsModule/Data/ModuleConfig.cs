// // Copyright (c) AUDI AG 2017
// //
// // Autor: Funk, Jan (I/EE-87)
// // Erstellt: 23.03.2017
namespace MeasurementsModule.Data
{
    using Core.IdexBaseModule;
    /// <summary>
    /// Klasse speichert die Konfiguration des Benutzers für dieses Modul
    /// </summary>
    public class ModuleConfig : IModuleState
    {
        /// <summary>
        /// Gibt an, ob die ODX-Namen angezeigt werden
        /// </summary>
        public bool ShowOdxNames { get; set; }

        /// <summary>
        /// Gibt an, ob die Service-Identifier angezeigt werden
        /// </summary>
        public bool ShowServiceIds { get; set; }

        /// <summary>
        /// Zykluszeit
        /// </summary>
        public int CycleTime { get; set; }
        
    }
}