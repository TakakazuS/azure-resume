using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class GetVisitorCounter
    {
        [Function("GetVisitorCounter")]
        public static MyOutputType Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [CosmosDBInput(databaseName: "AzureResume", collectionName: "Counter", ConnectionStringSetting = "AzureResumeConnectionString", Id = "1", PartitionKey = "1")] Counter counter,
            [CosmosDBInput(databaseName: "AzureResume", collectionName: "Counter", ConnectionStringSetting = "AzureResumeConnectionString", Id = "1", PartitionKey = "1")] out Counter UpdatedCounter,
            ILogger log)
        { 
            // Here is where the counter gets updated.
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            UpdatedCounter = counter;
            UpdatedCounter.Count += 1;

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            string jsonString = JsonSerializer.Serialize(counter);
            response.WriteString(jsonString);
        
            return new MyOutputType()
            {
                HttpResponse = response
            };
        }
    }
    
}