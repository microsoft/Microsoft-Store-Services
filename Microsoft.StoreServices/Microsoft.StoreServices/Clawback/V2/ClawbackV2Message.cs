//-----------------------------------------------------------------------------
// ClawbackV2Message.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Azure.Storage.Queues.Models;
using Jose;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Microsoft.StoreServices.Clawback.V2
{
    public class ClawbackV2Message
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ClawbackV2Message() {}

        /// <summary>
        /// Creates a ClawbackV2Message object from the values in an Azure QueueMessage
        /// </summary>
        /// <param name="message">Azure QueueMessage from a GetMessages() call</param>
        /// <exception cref="Exception"></exception>
        public ClawbackV2Message(QueueMessage message)
        {
            try
            {
                string jsonEvent = Encoding.UTF8.GetString(Base64Url.Decode(message.Body.ToString()));
                ClawbackEvent = JsonConvert.DeserializeObject<ClawbackV2Event>(jsonEvent, new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                });
            }
            catch(Exception ex)
            {
                throw new Exception($"ClawbaackV2Message - unable to deserialize message body {message.Body.ToString()}", ex);
            }
                
            MessageId  = message.MessageId;
            PopReceipt = message.PopReceipt;
            ExpiresOn  = message.ExpiresOn;
            InsertedOn = message.InsertedOn;
        }

        /// <summary>
        /// Creates a ClawbackV2Message object from the values in an Azure PeekedMessage
        /// </summary>
        /// <param name="message">Azure PeekedMessage from a PeekMessages() call</param>
        /// <exception cref="Exception"></exception>
        public ClawbackV2Message(PeekedMessage message)
        {
            try
            {
                string jsonEvent = Encoding.UTF8.GetString(Base64Url.Decode(message.Body.ToString()));
                ClawbackEvent = JsonConvert.DeserializeObject<ClawbackV2Event>(jsonEvent, new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"ClawbaackV2Message - unable to deserialize message body {message.Body.ToString()}", ex);
            }

            MessageId = message.MessageId;
            PopReceipt = "";
            ExpiresOn = message.ExpiresOn;
            InsertedOn = message.InsertedOn;
        }

        public ClawbackV2Event ClawbackEvent { get; set; }
        public string MessageId { get; set; }
        public string PopReceipt { get; set; }
        public DateTimeOffset? ExpiresOn { get; set; }
        public DateTimeOffset? InsertedOn { get; set; }
    }
}
