// Copyright (c) AUDI AG 2021
// 
// Autor: Hoepp, Michael (I/EE-87, extern)
// Erstellt: 12.01.2021

namespace SpecialTools.ProductionModeService.EventArguments
{
    using System;
    using IdexCommonCommunication.Interaction;

    /// <summary>
    /// Eventargumente zum Transportieren von sowohl der erhaltenen Response als auch dem ursprünglichen Request
    /// </summary>
    /// <typeparam name="TRequest">Spezifischer Typ des Requests auf den sich die Antwort bezieht</typeparam>
    /// <typeparam name="TResponse">Spezifischer Typ der erhaltenen Antwort</typeparam>
    internal class ResponseReceivedEventArgs<TRequest, TResponse> : EventArgs where TRequest : IRequest where TResponse : IResponse
    {
        /// <summary>
        /// Erstellt neue Event Argumente für eine Erhaltene Antwort
        /// </summary>
        /// <param name="aOriginalRequest">Anfrage auf die sich die Antwort bezieht</param>
        /// <param name="aReceivedResponse">Erhaltene Antwort</param>
        internal ResponseReceivedEventArgs(TRequest aOriginalRequest, TResponse aReceivedResponse)
        {
            Request = aOriginalRequest;
            Response = aReceivedResponse;
        }

        /// <summary>
        /// Anfrage auf die sich die Antwort bezieht
        /// </summary>
        internal TRequest Request { get; }

        /// <summary>
        /// Erhaltene Antwort
        /// </summary>
        internal TResponse Response { get; }
    }
}
