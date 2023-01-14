//-----------------------------------------------------------------------------
// StoreServicesClient.ClawbackV2.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.StoreServices.Clawback.V2;

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
        public async Task<ClawbackV2QueryEventsResponse> ClawbackV2QueryEventsAsync()
        {
            var sasToken = await _storeServicesTokenProvider.GetClawbackV2SASTokenAsync();
            var uri = new Uri(sasToken.Token);
            var eventQueueClient = new QueueClient(uri);

            var peekResult = await eventQueueClient.PeekMessagesAsync();

            var getResult = await eventQueueClient.ReceiveMessagesAsync();

            foreach(var message in getResult.Value)
            {
                var deleteMessageResult = await eventQueueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            }

            // return userClawback;
            return null;
        }
    }
}
