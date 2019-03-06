using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace testNetCoreFunctionApp
{
    public static class Function1
    {
        [FunctionName("helloWorldFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string _class = req.Query["class"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            _class = _class ?? data?.name;

            return _class != null
                ? (ActionResult)new OkObjectResult($"You are a, {_class}")
                : new BadRequestObjectResult("Please pass a class (wizard, warlock, thief...) on the query string or in the request body");
        }
    }
}
