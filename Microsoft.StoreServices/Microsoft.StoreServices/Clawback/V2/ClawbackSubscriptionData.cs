//-----------------------------------------------------------------------------
// ClawbackSubscriptionData.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices.Clawback.V2
{
    /// <summary>
    /// Represents specific information related to a subscription product that has triggered a Clawback event
    /// </summary>
    public class ClawbackSubscriptionData
    {
        /// <summary>
        /// Unique Id to be used with the Recurrence services to manage this subscription.
        /// </summary>   
        [JsonProperty("recurrenceId")]
        public string RecurrenceId { get; set; }

        /// <summary>
        /// UTC date time when the user's subscription period started
        /// </summary>
        [JsonProperty("durationIntervalStart")]
        public DateTimeOffset DurationIntervalStart { get; set; }

        /// <summary>
        /// Total duration of the user's purchased subscription (includes unused days)
        /// </summary>
        [JsonProperty("durationInDays")]
        public int DurationInDays { get; set; }

        /// <summary>
        /// How many days of the subscription were used before the refund.
        /// </summary>
        [JsonProperty("consumedDurationInDays")]
        public int ConsumedDurationInDays { get; set; }
    }
}
