// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EG-14, extern)
// Erstellt: 09.11.2021

namespace SpecialTools.ProductionModeService.Messaging.ProductionMode.Progress
{
    using System;
    using De.VwAudi.Common.Util.Communication.Serialization;
    using IdexCommonCommunication.Interaction;
    using JetBrains.Annotations;
    using Types;

    /// <summary>
    /// Fortschrittsnachricht dafür, dass eine Modifikation des Production Modes eines Teilnehmers gestartet wurde
    /// </summary>
    [AutoRegisterMessage]
    [PublicAPI]
    public class ProductionModeEcuModificationStartedProgress : IResponse
    {
        /// <summary>
        /// Baud Addresse des ECUs das nun modifiziert wird
        /// </summary>
        public uint BaudAddress { get; set; }

        /// <summary>
        /// Zielzustand
        /// </summary>
        public ProductionModeState TargetState { get; set; }

        ///<inheritdoc/>
        public Guid RequestId { get; set; }
    }
}
