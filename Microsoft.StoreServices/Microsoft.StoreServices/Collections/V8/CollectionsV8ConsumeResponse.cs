//-----------------------------------------------------------------------------
// CollectionsV8ConsumeResponse.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections.V8
{
    /// <summary>
    /// JSON response from a successful consume request
    /// </summary>
    public class CollectionsV8ConsumeResponse
    {
        /// <summary>
        /// ID that identifies this collection item from other items that the user owns. This ID is unique per product.
        /// </summary>
        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        /// <summary>
        /// The remaining balance of this consumable that the user owns.
        /// </summary>
        [JsonProperty("newQuantity")]
        public int NewQuantity { get; set; }

        /// <summary>
        /// The unique tracking ID that was provided in the request to track and validate that the fulfillment succeeded.
        /// </summary>
        [JsonProperty("trackingId")]
        public string TrackingId { get; set; }

        /// <summary>
        /// The ProductId / StoreId of the consumable that was fulfilled.
        /// </summary>
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        /// <summary>
        /// An array of ConsumeOrderTransactionContractV8 objects indicating the orders used to fulfill the consume request.
        /// This is only returned if the includeOrderIds parameter is set to true in the request.
        /// </summary>
        [JsonProperty("orderTransactions")]
        public List<ConsumeOrderTransactionContractV8> OrderTransactions { get; set; }
    }

    /// <summary>
    /// Ids related to the purchased order by the user that was used to fulfill the consume request.
    /// </summary>
    public class ConsumeOrderTransactionContractV8
    {
        /// <summary>
        /// Id of user's purchase order used to fulfill all or part of this consume request.
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Id of the line item the consumable was within the purchase order made by the user.
        /// This Id is more unique to a consumable purchase than OrderId as there can be multiple line item Ids per Order Id.
        /// </summary>
        [JsonProperty("orderLineItemId")]
        public string OrderLineItemId { get; set; }

        /// <summary>
        /// Amount of the consume request that was fulfilled by this specific OrderId / LineItemId
        /// </summary>
        [JsonProperty("quantityConsumed")]
        public int QuantityConsumed { get; set; }
    }

    /// <summary>
    /// Error data from the consume service if the consume request failed
    /// </summary>
    public class ConsumeErrorV8
    {
        /// <summary>
        /// Error code related to the consume service
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Additional data about the error
        /// </summary>
        [JsonProperty("data")]
        public List<string> Data { get; set; }

        /// <summary>
        /// Additional details about the error
        /// </summary>
        [JsonProperty("details")]
        public List<object> Details { get; set; }

        /// <summary>
        /// Message describing the error
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Source of the error being reported
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
    }

    /// <summary>
    /// JSON response body if there was an error executing the consume request
    /// </summary>
    public class CollectionsConsumeErrorResponseV8
    {
        /// <summary>
        /// Error code related to the consume service
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Additional data about the error
        /// </summary>
        [JsonProperty("data")]
        public List<string> Data { get; set; }

        /// <summary>
        /// Additional details about the error
        /// </summary>
        [JsonProperty("details")]
        public List<object> Details { get; set; }

        /// <summary>
        /// Error data from the consume service if the consume request failed
        /// </summary>
        [JsonProperty("innererror")]
        public ConsumeErrorV8 InnerError { get; set; }

        /// <summary>
        /// Message describing the error
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Source of the error being reported
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
    }
}
