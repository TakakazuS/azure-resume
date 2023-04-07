namespace tests;

public class NewBaseType
{
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

public class TestFactory : NewBaseType
{
    public static object? NullLoggerFactory { get; private set; }

    public static HttpRequest CreateHttpRequest()
    {
        var context = new DefaultHttpContext();
        var request = context.Request;
        return (HttpRequest)request;
    }
}

public class HttpRequest
{
}