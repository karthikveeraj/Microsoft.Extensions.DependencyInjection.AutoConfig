namespace Microsoft.Extensions.DependencyInjection.AutoConfig.Tests
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Demo2 : IDemo
    {
        public string Message { get; set; }
        public string DemoLog() => GetType().Name;
    }
}
