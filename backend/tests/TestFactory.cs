using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;

namespace tests
{
    public class TestFactory
    {
        public static HttpRequestData CreateHttpRequest()
        {
            var request = new HttpRequestDataWrapper();
            request.Method = "GET";
            request.Url = new Uri("https://www.takakazu.me");
            var headers = new Dictionary<string, StringValues>();
            headers.Add("User-Agent", "Test");
            request.Headers = new HttpHeaders(headers);
            return request;
        }

        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            ILogger logger;

            if (type == LoggerTypes.List)
            {
                logger = new ListLogger();
            }
            else
            {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }

            return logger;
        }
    }
}
