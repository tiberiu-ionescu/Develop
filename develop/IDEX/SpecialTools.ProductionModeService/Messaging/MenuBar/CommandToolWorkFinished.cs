// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EG-14, extern)
// Erstellt: 08.11.2021

namespace SpecialTools.ProductionModeService.Messaging.MenuBar
{
    using De.VwAudi.Common.Util.Communication.Interfaces;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using JetBrains.Annotations;
    using Properties;

    /// <summary>
    /// Message Klasse um zu verarbeiten, dass ein Tool gerade die Berechnungen beendet hat
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public sealed class CommandToolWorkFinished : IMessage
    {
        /// <summary>
        /// Id von dem Tool welches das Command gesendet hat
        /// </summary>
        public string ToolId { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CommandToolWorkFinished()
        {
            ToolId = Resources.TOOL_NAME;
        }
    }
}
