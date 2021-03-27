namespace Microsoft.Extensions.DependencyInjection.AutoConfig.Tests
{
    using Xunit;
    using System;
    using System.IO;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public class ServiceCollectionExtensionsTest
    {
        [Fact, Trait(nameof(DependencyInjectionServiceCollectionExtensions), "AddAutoTypeResolver")]
        public void AddAutoTypeResolver_Success()
        {
            var configuration = new ConfigurationBuilder()
                                   .SetBasePath(AppContext.BaseDirectory)
                                   .AddJsonFile(".\\TestConfigurationFiles\\testappsettings.json", optional: false, reloadOnChange: true)
                                   .Build() ?? throw new NullReferenceException("Failed to create 'Configuration' instance.");
            var provider = new ServiceCollection()
                                   .AddAutoTypeResolver(configuration)                                  
                                   .BuildServiceProvider() ?? throw new NullReferenceException("Failed to create the 'ServiceProvider' instance.");
            Assert.NotNull(provider);
            Assert.NotNull(provider.GetService<IDemo>());
            Assert.IsType<Demo1>(provider.GetService<IDemo>("Demo1"));
            Assert.IsType<Demo2>(provider.GetService<IDemo>("Demo2"));

            // Singleton test
            var service = provider.GetService<ITypeRegistration>() as TypeRegistration;
            service.Name = "TestName1";
            Assert.Equal(service.Name, provider.GetService<ITypeRegistration>().Name);

            // Transient test
            var message1 = provider.GetService<IDemoMessage>() as DemoMessage;
            message1.Message = "Hello";
            Assert.Null(provider.GetService<IDemoMessage>().Message);

            // Scoped test
            var scope1 = provider.GetService<IScopedMessage>() as ScopedMessage;
            scope1.Message = "Scope1";
            Assert.Equal(scope1.Message, provider.GetService<IScopedMessage>().Message);
        }

        [Fact, Trait(nameof(DependencyInjectionServiceCollectionExtensions), "AddAutoTypeResolver")]
        public void AddAutoTypeResolver_ArgumentNullException_ConfigNull()
        {
            Assert.Throws<ArgumentNullException>("config", () => new ServiceCollection().AddAutoTypeResolver(null));
        }

        [Fact, Trait(nameof(DependencyInjectionServiceCollectionExtensions), "AddAutoTypeResolver")]
        public void AddAutoTypeResolver_TypeLoadException_MissingDiConfig()
        {
            var configuration = new ConfigurationBuilder()
                       .SetBasePath(AppContext.BaseDirectory)
                       .AddJsonFile(".\\TestConfigurationFiles\\testappsettings-missing-diconfig.json", optional: false, reloadOnChange: true)
                       .Build() ?? throw new NullReferenceException("Failed to create 'Configuration' instance.");
            Assert.Throws<TypeLoadException>(() => new ServiceCollection().AddAutoTypeResolver(configuration));
        }

        [Fact, Trait(nameof(DependencyInjectionServiceCollectionExtensions), "AddAutoTypeResolver")]
        public void ServiceCollectionExtensions_AddAutoTypeResolver_MissingRegisterAssembliesException()
        {
            var configuration = new ConfigurationBuilder()
                                   .SetBasePath(AppContext.BaseDirectory)
                                   .AddJsonFile(".\\TestConfigurationFiles\\testappsettings-missing-registerAssemblies.json", optional: false, reloadOnChange: true)
                                   .Build() ?? throw new NullReferenceException("Failed to create 'Configuration' instance.");

            Assert.Throws<FileNotFoundException>(()=> new ServiceCollection()
                                                       .AddAutoTypeResolver(configuration)
                                                       .BuildServiceProvider() ?? throw new NullReferenceException("Failed to create the 'ServiceProvider' instance."));
        }

        //[Fact, Trait(nameof(DependencyInjectionServiceCollectionExtensions), "AddAutoTypeResolver")]
        //public void ServiceCollectionExtensions_AddAutoTypeResolver_MissingRegisterTypeException()
        //{
        //    var configuration = new ConfigurationBuilder()
        //                           .SetBasePath(AppContext.BaseDirectory)
        //                           .AddJsonFile(".\\TestConfigurationFiles\\testappsettings-missing-registerType.json", optional: false, reloadOnChange: true)
        //                           .Build() ?? throw new NullReferenceException("Failed to create 'Configuration' instance.");

        //    Assert.Throws<TypeLoadException>(() => new ServiceCollection()
        //                                            .AddAutoTypeResolver(configuration)
        //                                            .BuildServiceProvider() ?? throw new NullReferenceException("Failed to create the 'ServiceProvider' instance."));
        //}

    }
}
