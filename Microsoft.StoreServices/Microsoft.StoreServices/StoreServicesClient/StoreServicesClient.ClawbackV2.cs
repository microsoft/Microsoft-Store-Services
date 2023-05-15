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
        /// parameters of the request.  Any returned events are put in a timeout and will
        /// not be returned in any follow up calls for 30 seconds. 
        /// </summary>
        /// <param name="maxMessages">Max number of messages to receive from the Clawback v2 message queue.  Default value is 1, Max value is 32</param>
        /// <returns></returns>
        public async Task<List<ClawbackV2Message>> ClawbackV2QueryEventsAsync(int? maxMessages = null)
        {
            if (maxMessages == null)
            {
                maxMessages = 1;    // default return value when a maxMessages parameter is not passed to the
                                    // Azure Message Queue
            }

            if (maxMessages <= 0 || maxMessages > 32)
            {
                throw new ArgumentException($"{nameof(maxMessages)} has value of {maxMessages}, must be between 1 - 32", nameof(maxMessages));
            }

            var sasToken = await _storeServicesTokenProvider.GetClawbackV2SASTokenAsync();
            var uri = new Uri(sasToken.Token);
            var eventQueueClient = new QueueClient(uri);

            var getMessagesResult = await eventQueueClient.ReceiveMessagesAsync(maxMessages);

            var clawbackMessages = new List<ClawbackV2Message>();
            foreach (var queueMessage in getMessagesResult.Value)
            {
                var clawbackMessage = new ClawbackV2Message(queueMessage);
                clawbackMessages.Add(clawbackMessage);
            }

            return clawbackMessages;
        }

        /// <summary>
        /// Query for the user's refunded products from the Clawback service based on the 
        /// parameters of the request.  Peek does not put any retrieved messages in a timeout.
        /// </summary>
        /// <param name="maxMessages">Max number of messages to receive from the Clawback v2 message queue.  Default value is 1, Max value is 32</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<ClawbackV2Message>> ClawbackV2PeekEventsAsync(int? maxMessages = null)
        {
            if (maxMessages == null)
            {
                maxMessages = 1;    // default return value when a maxMessages parameter is not passed to the
                                    // Azure Message Queue
            }

            if (maxMessages <= 0 || maxMessages > 32)
            {
                throw new ArgumentException($"{nameof(maxMessages)} has value of {maxMessages}, must be between 1 - 32", nameof(maxMessages));
            }

            var sasToken = await _storeServicesTokenProvider.GetClawbackV2SASTokenAsync();
            var uri = new Uri(sasToken.Token);
            var eventQueueClient = new QueueClient(uri);

            var getMessagesResult = await eventQueueClient.PeekMessagesAsync(maxMessages);

            var clawbackMessages = new List<ClawbackV2Message>();
            foreach (var queueMessage in getMessagesResult.Value)
            {
                var clawbackMessage = new ClawbackV2Message(queueMessage);
                clawbackMessages.Add(clawbackMessage);
            }

            return clawbackMessages;
        }

        /// <summary>
        /// Deletes the provided message from the Clawback message queue.
        /// </summary>
        /// <param name="messageToDelete">Which message should be deleted from the message queue.</param>
        /// <returns>HTTP status code from the delete request to the Azure Message Queue</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<int> ClawbackV2DeleteMessageAsync(ClawbackV2Message messageToDelete)
        {
            if (messageToDelete == null)
            {
                throw new ArgumentException($"{nameof(messageToDelete)} cannot be null", nameof(messageToDelete));
            }

            if (string.IsNullOrEmpty(messageToDelete.PopReceipt))
            {
                throw new ArgumentException($"{nameof(messageToDelete.PopReceipt)} cannot be empty", nameof(messageToDelete.PopReceipt));
            }

            if (string.IsNullOrEmpty(messageToDelete.MessageId))
            {
                throw new ArgumentException($"{nameof(messageToDelete.MessageId)} cannot be empty", nameof(messageToDelete.MessageId));
            }

            var sasToken = await _storeServicesTokenProvider.GetClawbackV2SASTokenAsync();
            var uri = new Uri(sasToken.Token);
            var eventQueueClient = new QueueClient(uri);

            var deleteResponse = await eventQueueClient.DeleteMessageAsync(messageToDelete.MessageId, messageToDelete.PopReceipt);

            if (deleteResponse.IsError)
            {
                throw new Exception($"Error deleting MessageId {messageToDelete.MessageId} from ClawbackV2 queue. {deleteResponse.ReasonPhrase}");
            }

            return deleteResponse.Status;
        }
    }
}
