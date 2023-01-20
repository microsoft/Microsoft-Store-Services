//-----------------------------------------------------------------------------
// IStoreServicesClient.Clawback.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.StoreServices.Collections.V8;
using Microsoft.StoreServices.Collections.V9;
using Microsoft.StoreServices.Clawback.V1;
using Microsoft.StoreServices.Clawback.V2;
using System.Collections.Generic;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// Provides an interface to create a client that will manage the requests and responses from the
    /// Microsoft Store Services.
    /// </summary>
    public interface IStoreServicesClient : IDisposable
    {
        /// <summary>
        /// Query the user's collections information from the V8 endpoint and returns any items  
        /// that meet the query parameters.  These items are directly owned by the user who's 
        /// UserCollectionsId we are passing with the QueryParameters.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CollectionsV8QueryResponse> CollectionsV8QueryAsync(CollectionsV8QueryRequest request);

        /// <summary>
        /// Query the user's collections information from the V9 endpoint and returns any items  
        /// that meet the query parameters.  These items are directly owned by the user who's 
        /// UserCollectionsId we are passing with the QueryParameters.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CollectionsV9QueryResponse> CollectionsV9QueryAsync(CollectionsV9QueryRequest request);

        /// <summary>
        /// Consumes a specified quantity of a product from a user's active balance in the store.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CollectionsV8ConsumeResponse> CollectionsConsumeAsync(CollectionsV8ConsumeRequest request);

        /// <summary>
        /// Query's the Clawback information for the user based on the parameters object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ClawbackV1QueryResponse> ClawbackV1QueryAsync(ClawbackV1QueryRequest request);

        /// <summary>
        /// Queries the Clawback v2 service for refund events
        /// </summary>
        /// <returns></returns>
        Task<List<ClawbackV2Message>> ClawbackV2QueryEventsAsync(int? maxMessages = null);

        /// <summary>
        /// Queries the Clawback v2 service and peeks at the messages
        /// </summary>
        /// <returns></returns>
        Task<List<ClawbackV2Message>> ClawbackV2PeekEventsAsync(int? maxMessages = null);

        /// <summary>
        /// Deletes the provided message from the Clawback message queue.
        /// </summary>
        /// <param name="messageToDelete">Which message should be deleted from the message queue.</param>
        /// <returns>HTTP status code from the delete request to the Azure Message Queue</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        Task<int> ClawbackV2DeleteMessageAsync(ClawbackV2Message messageToDelete);

        /// <summary>
        /// Query's the Recurrence information for the user based on the parameters object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RecurrenceQueryResponse> RecurrenceQueryAsync(RecurrenceQueryRequest request);

        /// <summary>
        /// Allows the caller to change the billing state of an existing user's
        /// subscription.  Extend its date, cancel, enable/disable auto-renew, or
        /// refund the subscription.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RecurrenceChangeResponse> RecurrenceChangeAysnc(RecurrenceChangeRequest request);

        /// <summary>
        /// Provides the Service Access Token from the IAccessTokenControler used when the
        /// Client was configured.
        /// </summary>
        /// <returns></returns>
        Task<AccessToken> GetServiceAccessTokenAsync();

        /// <summary>
        /// Provides the Collections Access Token from the IAccessTokenControler used when the
        /// Client was configured.
        /// </summary>
        /// <returns></returns>
        Task<AccessToken> GetCollectionsAccessTokenAsync();

        /// <summary>
        /// Provides the Purchase Access Token from the IAccessTokenControler used when the
        /// Client was configured.
        /// </summary>
        /// <returns></returns>
        Task<AccessToken> GetPurchaseAccessTokenAsync();       
    }
}
