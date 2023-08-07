using AwesomeShop.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AwesomeShop.Infrastructure.Integrations.Azure.Functions;

public static class AddOrder
{
    [FunctionName("AddOrder")]
    [return: ServiceBus("order-created", Connection = "ServiceBusCs")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
        HttpRequest req,
        ILogger log)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<Order>(requestBody);

        log.LogInformation($"Order {data.id} created.");

        return new OkObjectResult(data);
    }
}