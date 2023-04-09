using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function;

public class GetVisitorCounter
{
    private readonly ILogger _logger;

    public GetVisitorCounter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<GetVisitorCounter>();
    }

    [Function("GetVisitorCounter")]
    public MyOutputType Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [CosmosDBInput(databaseName: "AzureResume", collectionName: 
    "Counter", ConnectionStringSetting = "AzureResumeConnectionString", Id = "1",
            PartitionKey = "1")] Counter counter)
    {
        
        counter.Count += 1;
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        string jsonString = JsonSerializer.Serialize(counter);
        response.WriteString(jsonString);
        
        return new MyOutputType()
        {
            UpdatedCounter = counter,
            HttpResponse = response
        };
    }
}