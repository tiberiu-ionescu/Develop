// Copyright (c) AUDI AG 2020
// 
// Autor: Josovic, Nenad (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService
{
    /// <summary>
    /// Klasse enthält die Tool Informationen
    /// </summary>
    internal static class ToolInformation
    {
        /// <summary>
        /// Id des Tool 
        /// </summary>
        public static string Id { get; internal set; }

        /// <summary>
        /// Zu dem Tool gehörendes Icon im Bitmap Format
        /// </summary>
        public static byte[] ImageBitmap { get; internal set; }

        /// <summary>
        /// Zu dem Tool gehörendes Icon im SVG Format
        /// </summary>
        public static byte[] ImageSvg { get; internal set; }

        /// <summary>
        /// Gruppenname in dem das Tool einsortiert wird lokalisiert
        /// </summary>
        public static string LocalizedGroupName { get; internal set; }

        /// <summary>
        /// Beschreibung des Tools lokalisiert
        /// </summary>
        public static string LocalizedToolDescription { get; internal set; }

        /// <summary>
        /// Toolname lokalisiert
        /// </summary>
        public static string LocalizedToolName { get; internal set; }

        /// <summary>
        /// Toolname interne Bezeichnung
        /// </summary>
        public static string ToolName { get; internal set; }
    }
}
