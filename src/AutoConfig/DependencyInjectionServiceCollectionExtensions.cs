namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Adds an extension method <c>AddAutoTypeResolver</c> to <see cref="IServiceCollection"/> type to register the types automatically.
    /// </summary>
    public static class DependencyInjectionServiceCollectionExtensions
    {
        /// <summary>
        /// Get service of type T from the System.IServiceProvider for the given <paramref name="typeName"/>.
        /// </summary>
        /// <typeparam name="TSource"> The type of service object to get.</typeparam>
        /// <param name="provider"> The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>A service object of type T or null if there is no such service.</returns>
        public static TSource GetService<TSource>(this IServiceProvider provider, string typeName)
        {
            return provider.GetServices<TSource>().FirstOrDefault(type => type.GetType().Name == typeName);
        }

        /// <summary>
        /// Registers the types specified in the configuration file automatically.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoTypeResolver(this IServiceCollection services, IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var mappingsConfig = config.GetSection(nameof(DependencyInjectionConfig)).Get<DependencyInjectionConfig>();
            if (mappingsConfig == null)
            {
                throw new TypeLoadException($"'{nameof(DependencyInjectionConfig)}' section doesn't exist or it is not well formed.");
            }

            var assemblyTypeMap = DependencyInjectionUtils.GetTypeMapsFromAssemblies(mappingsConfig.RegisterAssemblies);
            DependencyInjectionUtils.UpdateRegisteredTypes(mappingsConfig, assemblyTypeMap);

            foreach (var map in assemblyTypeMap.SelectMany(item => item.Value.Select(t => (item.Key, t.AssemblyType, t.Type))))
            {
                switch (map.Type)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient(Type.GetType(map.Key), map.AssemblyType);
                        break;

                    case ServiceLifetime.Scoped:
                        services.AddScoped(Type.GetType(map.Key), map.AssemblyType);
                        break;

                    case ServiceLifetime.Singleton:
                        services.AddSingleton(Type.GetType(map.Key), map.AssemblyType);
                        break;
                }
            }

            return services;
        }
    }
}
