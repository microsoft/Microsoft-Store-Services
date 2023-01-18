//-----------------------------------------------------------------------------
// StoreServicesClient.ClawbackV2.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Azure.Storage.Queues;
using Microsoft.StoreServices.Clawback.V2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
        /// <summary>
        /// Query for the user's refunded products from the Clawback service based on the 
        /// parameters of the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ClawbackV2Message>> ClawbackV2QueryEventsAsync()
        {
            var sasToken = await _storeServicesTokenProvider.GetClawbackV2SASTokenAsync();
            var uri = new Uri(sasToken.Token);
            var eventQueueClient = new QueueClient(uri);

            var getMessagesResult = await eventQueueClient.ReceiveMessagesAsync();

            var clawbackMessages = new List<ClawbackV2Message>();
            foreach(var queueMessage in getMessagesResult.Value)
            {
                var clawbackMessage = new ClawbackV2Message(queueMessage);
                clawbackMessages.Add(clawbackMessage);
            }

            return clawbackMessages;
        }

        public async Task<List<ClawbackV2Message>> ClawbackV2PeekEventsAsync()
        {
            var sasToken = await _storeServicesTokenProvider.GetClawbackV2SASTokenAsync();
            var uri = new Uri(sasToken.Token);
            var eventQueueClient = new QueueClient(uri);

            var getMessagesResult = await eventQueueClient.PeekMessagesAsync();

            var clawbackMessages = new List<ClawbackV2Message>();
            foreach (var queueMessage in getMessagesResult.Value)
            {
                var clawbackMessage = new ClawbackV2Message(queueMessage);
                clawbackMessages.Add(clawbackMessage);
            }

            return clawbackMessages;
        }
    }
}
