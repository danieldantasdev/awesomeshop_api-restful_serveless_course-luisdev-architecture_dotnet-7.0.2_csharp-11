﻿using AwesomeShop.Core.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AwesomeShop.Infrastructure.Integrations.Azure.Functions;

public class ProcessPayment
{
    [FunctionName("ProcessPayment")]
    public void Run(
        [ServiceBusTrigger("order-created", Connection = "ServiceBusCs")]
        string myQueueItem,
        [CosmosDB(
            "ecommerce-serverless",
            "order-receipts",
            ConnectionStringSetting = "CosmosDbCs")]
        out dynamic document,
        ILogger log)
    {
        var order = JsonConvert.DeserializeObject<Order>(myQueueItem);

        document = new PaymentReceipt
            { id = new Random().Next(1, 10000).ToString(), orderId = order.id, paidAt = DateTime.Now };

        log.LogInformation($"Payment processed for Order");
    }
}