# Microsoft.Extensions.DependencyInjection.AutoConfig
When you work on bigger projects, you need to write long list repetative lines of code to register interface types and their realizations. I wanted to avoid such piece of code and automate it so that we don't have to worry about registering specific types.

Instead of registering multiple types individually, AutoConfig provides an option to register the whole assembly itself which internatlly identifiers all the interfaces and their implementors and registers them. If there is a more than one implementation for a specific interface, it gives an option to mention which type you would like to resolve.

### Installing NuGet package

Visual Studio:
```powershell
PM> Install-Package Microsoft.Extensions.DependencyInjection.AutoConfig
```

.NET Core CLI:
```bash
dotnet add package Microsoft.Extensions.DependencyInjection.AutoConfig
```

## Sample appSetings.config file
```json
{
  "dependencyInjectionConfig": {
    "registerAssemblies": [
      { "order": 1, "type": "Transient", "name": "Microsoft.Extensions.DependencyInjection.AutoConfig.Tests.dll" }
    ],

    "registerTypes": [
      {
        "from": "Microsoft.Extensions.DependencyInjection.ITypeRegistration, Microsoft.Extensions.DependencyInjection.AutoConfig",
        "to":   "Microsoft.Extensions.DependencyInjection.TypeRegistration,  Microsoft.Extensions.DependencyInjection.AutoConfig",
        "type": "Singleton"
      },
      {
        "from": "Microsoft.Extensions.DependencyInjection.AutoConfig.Tests.IScopedMessage, Microsoft.Extensions.DependencyInjection.AutoConfig.Tests",
        "to":   "Microsoft.Extensions.DependencyInjection.AutoConfig.Tests.ScopedMessage,  Microsoft.Extensions.DependencyInjection.AutoConfig.Tests",
        "type": "Scoped"
      }
    ]
  }
}
```

## Using AutoConfig
```C#
namespace Honeywell.Isp.Services.Esm.Host.CoreConsole
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                                    .SetBasePath(AppContext.BaseDirectory)
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .Build() ?? throw new NullReferenceException("Failed to create 'Configuration' instance.");

                var provider = new ServiceCollection()
                                    .AddAutoTypeResolver(configuration) <--- Use this statement for automatic registrations.
                                    .BuildServiceProvider() ?? throw new NullReferenceException("Failed to create the 'ServiceProvider' instance.");
                                    
                // Now, use provider here onwards to resolve registered types!
                // EXAMPLE 1 - Get auto registered types.
                var message = provider.GetService<IDemoMessage>();
                // EXAMPLE 2 - Get the specific type when multiple implementations available in the specified assembly. (By speficiying class name)
                var demoInstance = provider.GetService<IDemo>("Demo1");
            }
            catch (Exception ex)
            {
                // ... Handler error.
            }
        }
    }
}
```
