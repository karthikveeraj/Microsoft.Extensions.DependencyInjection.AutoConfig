namespace Microsoft.Extensions.DependencyInjection.AutoConfig.Tests
{
    public interface IDemoMessage
    {
        string Message { get; }
        string DemoLog();
    }
}
