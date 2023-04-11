using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace tests
{
    public class TestCounter
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async Task Http_trigger_should_return_known_string()
        {
            var counter = new Company.Function.Counter();
            counter.Id = "1";
            counter.Count = 2;
            var httpRequestData = new HttpRequestData(
                HttpMethod.Get,
                new Uri("http://localhost:7071/api/GetVisitorCounter"),
                headers: null,
                body: null,
                FunctionContext.None);
            var response = (HttpResponseMessage) Company.Function.GetVisitorCounter.Run(httpRequestData, counter, logger);
            Assert.Equal(3, counter.Count);
        }

    }
}