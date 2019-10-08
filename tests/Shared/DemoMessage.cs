namespace Microsoft.Extensions.DependencyInjection.AutoConfig.Tests
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DemoMessage : IDemoMessage
    {
        public string Message { get; set; }
        public string DemoLog() => GetType().Name;
    }
}
