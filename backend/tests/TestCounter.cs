namespace tests;

public interface ITestCounter
{
    bool Equals(object? obj);
    int GetHashCode();
    void Http_trigger_should_return_known_string();
    string? ToString();
}

public class TestCounter : ITestCounter
{
    private readonly ILogger logger = TestFactory.CreateLogger();

    public TestCounter()
    {
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    [Fact]
    public void Http_trigger_should_return_known_string()
    {
        var counter = new Company.Function.Counter();
        counter.Id = "index";
        counter.Count = 2;
        var request = TestFactory.CreateHttpRequest();
        var response = (HttpResponseMessage)Company.Function.GetResumeCounter.Run(request, counter, out counter, logger);
        Assert.Equal(3, counter.Count);
    }

    public override string? ToString()
    {
        return base.ToString();
    }
}

internal class FactAttribute : Attribute
{
}