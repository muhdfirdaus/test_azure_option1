using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Firdaus.Calculate
{
    
    public static class CalculatorFunction
    {
        [FunctionName("Calculate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string operation = req.Query["operation"];
            double num1 = Convert.ToDouble(req.Query["num1"]);
            double num2 = Convert.ToDouble(req.Query["num2"]);

            double result;
            string errorMessage = "";

            switch (operation.ToLower())
            {
                case "add":
                    result = num1 + num2;
                    break;
                case "subtract":
                    result = num1 - num2;
                    break;
                case "multiply":
                    result = num1 * num2;
                    break;
                case "divide":
                    if (num2 == 0)
                    {
                        return new BadRequestObjectResult("Error: Division by zero.");
                    }
                    result = num1 / num2;
                    break;
                default:
                    return new BadRequestObjectResult("Error: Invalid operation.");
            }

            return new OkObjectResult(result);
        }
    }
    // public static class HttpTrigger1
    // {
    //     [FunctionName("HttpTrigger1")]
    //     public static async Task<IActionResult> Run(
    //         [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
    //         ILogger log)
    //     {
    //         log.LogInformation("C# HTTP trigger function processed a request.");

    //         string name = req.Query["name"];

    //         string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    //         dynamic data = JsonConvert.DeserializeObject(requestBody);
    //         name = name ?? data?.name;

    //         string responseMessage = string.IsNullOrEmpty(name)
    //             ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
    //             : $"Hello, {name}. This HTTP triggered function executed successfully.";

    //         return new OkObjectResult(responseMessage);
    //     }
    // }
}
