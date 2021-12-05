// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EG-14, extern)
// Erstellt: 08.11.2021

namespace SpecialTools.ProductionModeService.Types
{
    using JetBrains.Annotations;
    using Messaging.ProductionMode.Types;
    using Properties;

    /// <summary>
    /// Datenobjekt für das Devexpress Grid, das ein ECU repräsentiert, das den Produktionsmodus unterstützt und entsprechend einen Zustand hat
    /// </summary>
    [PublicAPI]
    public class EcuState
    {
        /// <summary>
        /// Erstellt einen Produktionsmoduszustand anhand des Message Inhalts 
        /// </summary>
        /// <param name="aEcuState">Zustand des Steuergeräts aus der Message</param>
        internal EcuState(EcuProductionModeState aEcuState):this(aEcuState, false)
        {
        }

        /// <summary>
        /// Zustand des Steuergeräts anhand des Message Inhalts anhand des Message Inhalts und dem Flag, ob die Umschaltung durch den Master erwartet wird
        /// </summary>
        /// <param name="aEcuState">Zustand des Steuergeräts aus der Message</param>
        /// <param name="aWaitingForMasterFlip"><c>true</c> wenn das Steuergerät noch nicht umgeschalten ist, aber auf die Master Umschaltung wartet</param>
        internal EcuState(EcuProductionModeState aEcuState, bool aWaitingForMasterFlip)
        {
            _wrappedState = aEcuState;
            _waitingForMasterFlip = aWaitingForMasterFlip;
        }

        /// <summary>
        /// Baud Addresse des Steuergeräts
        /// </summary>
        public uint BaudAddress => _wrappedState.BaudAddress;

        /// <summary>
        /// ECU Status
        /// </summary>
        private readonly EcuProductionModeState _wrappedState;

        /// <summary>
        /// Übersetzter Name des Steuergeräts
        /// </summary>
        public string LocalizedEcuName => _wrappedState.LocalizedEcuName;

        /// <summary>
        /// Übersetzter Antwort Typ
        /// </summary>
        public string LocalizedResult
        {
            get
            {
                switch (_wrappedState.ResultType)
                {
                    case ProductionModeResultType.AccessDenied:
                        return Resources.RESULT_ACCESS_DENIED;
                    case ProductionModeResultType.AdaptionMissing:
                        return Resources.RESULT_ADAPTATION_MISSING;
                    case ProductionModeResultType.AdaptionWriteNrc:
                        return Resources.RESULT_ADAPTATION_WRITE_NRC;
                    case ProductionModeResultType.CustomHexServiceNrc:
                        return Resources.RESULT_CUSTOM_HEX_SERVICE_NRC;
                    case ProductionModeResultType.MeasurementBlockMissing:
                        return Resources.RESULT_MEASUREMENT_BLOCK_MISSING;
                    case ProductionModeResultType.NoError:
                        return Resources.RESULT_NO_ERROR;
                    case ProductionModeResultType.PModeMasterNotSet:
                        return Resources.RESULT_PMODE_MASTER_NOT_SET;
                    case ProductionModeResultType.RestartFailed:
                        return Resources.RESULT_RESTART_FAILED;
                    case ProductionModeResultType.StateDidNotChange:
                        return Resources.RESULT_STATE_DID_NOT_CHANGE;
                    case ProductionModeResultType.UnknownError:
                        return Resources.RESULT_UNKNOWN_ERROR;
                    case ProductionModeResultType.AdaptionWriteFailed:
                        return Resources.RESULT_ADAPTATION_WRITE_FAILED;
                    case ProductionModeResultType.ConnectionFailed:
                        return Resources.RESULT_CONNECTION_FAILED;
                    case ProductionModeResultType.DiagnosticTaskCreationFailed:
                        return Resources.RESULT_DIAGNOSTIC_TASK_CREATION_FAILED;
                    case ProductionModeResultType.CustomHexServiceDispatchFailed:
                        return Resources.RESULT_CUSTOM_HEX_SERVICE_DISPATCH_FAILED;
                    case ProductionModeResultType.SessionChangeFailed:
                        return Resources.RESULT_SESSION_CHANGE_FAILED;
                }

                return _wrappedState.ResultType.ToString();
            }
        }

        /// <summary>
        /// Wartet darauf, dass der Master gesetzt wird
        /// </summary>
        private readonly bool _waitingForMasterFlip;

        /// <summary>
        /// Übersetzter Status
        /// </summary>
        public string LocalizedStatus
        {
            get
            {
                string localizedStatus;
                if (_waitingForMasterFlip)
                {
                    localizedStatus = Resources.STATE_WAITING_FOR_MASTER;
                }
                else
                {
                    switch (_wrappedState.State)
                    {
                        case ProductionModeState.Active:
                            localizedStatus = Resources.STATE_ACTIVE;
                            break;
                        case ProductionModeState.Inactive:
                            localizedStatus = Resources.STATE_INACTIVE;
                            break;
                        default:
                            localizedStatus = Resources.STATE_UNKNOWN;
                            break;
                    }
                }

                return localizedStatus;
            }
        }
    }
}
