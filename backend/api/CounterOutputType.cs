using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Company.Function;
public class MyOutputType
{
    [CosmosDBOutput(databaseName: "AzureResume", collectionName: "Counter", ConnectionStringSetting = "AzureResumeConnectionString")]
    public Counter? UpdatedCounter { get; set; }
    public HttpResponseData? HttpResponse { get; set; }
}