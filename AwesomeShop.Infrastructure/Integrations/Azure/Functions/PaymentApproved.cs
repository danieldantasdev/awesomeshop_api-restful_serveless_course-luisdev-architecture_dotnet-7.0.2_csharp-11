using AwesomeShop.Core.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AwesomeShop.Infrastructure.Integrations.Azure.Functions;

public static class PaymentApproved
{
    [FunctionName("PaymentApproved")]
    public static void Run([CosmosDBTrigger(
            "ecommerce-serverless",
            "order-receipts",
            ConnectionStringSetting = "CosmosDbCs",
            LeaseCollectionName = "leases")]
        IReadOnlyList<PaymentReceipt> input,
        ILogger log)
    {
        if (input != null && input.Count > 0)
            foreach (var item in input)
                log.LogInformation($"Payment receipt {item.id}, paid at {item.paidAt}");
    }
}