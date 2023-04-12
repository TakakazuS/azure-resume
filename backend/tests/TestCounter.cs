using Company.Function;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

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
            var request = TestFactory.CreateHttpRequest();
            var function = new Company.Function.GetVisitorCounter(NullLoggerFactory.Instance);
            var response = function.Run(request, counter);
            Assert.Equal(3, counter.Count);
        }
    }
}