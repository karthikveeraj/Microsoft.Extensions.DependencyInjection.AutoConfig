namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal static class DependencyInjectionUtils
    {
        internal static Dictionary<string, List<AssemblyTypeMap>> GetTypeMapsFromAssemblies(ICollection<NameType> registeredAssemblies)
        {
            var foundContracts = new List<Type>();
            var assemblyTypeMap = new Dictionary<string, List<AssemblyTypeMap>>();

            foreach (var map in registeredAssemblies.OrderBy(map => map.Order))
            {
                // TODO : Using Assembly.Load causing some issues now. Until I find the fix, I'll continue to use Assembly.LoadFrom.
                var types = Assembly.LoadFrom($"{AppContext.BaseDirectory}\\{map.Name}").GetTypes();
                foundContracts.AddRange(types.Where(type => type.IsInterface));
                foreach (var interfaceType in foundContracts)
                {
                    var list = types.Where(targetType => targetType.IsClass && !targetType.IsAbstract && interfaceType.IsAssignableFrom(targetType));
                    if (!list.Any())
                    {
                        continue;
                    }

                    if (assemblyTypeMap.ContainsKey(interfaceType.AssemblyQualifiedName))
                    {
                        assemblyTypeMap[interfaceType.AssemblyQualifiedName].AddRange(list.Select(item => new AssemblyTypeMap(item, map.Type, map.Name)));
                    }
                    else
                    {
                        assemblyTypeMap[interfaceType.AssemblyQualifiedName] = list.Select(item => new AssemblyTypeMap(item, map.Type, map.Name)).ToList();
                    }
                }
            }

            return assemblyTypeMap;
        }

        internal static void UpdateRegisteredTypes(DependencyInjectionConfig mappingsConfig, Dictionary<string, List<AssemblyTypeMap>> assemblyTypeMap)
        {
            foreach (var mapping in mappingsConfig.RegisterTypes)
            {
                var fromType = Type.GetType(mapping.From, true);
                var toType = Type.GetType(mapping.To, true);

                var typeMap = new AssemblyTypeMap(toType, mapping.Type, mapping.Name);
                if (assemblyTypeMap.ContainsKey(fromType.AssemblyQualifiedName))
                {
                    if (string.IsNullOrWhiteSpace(mapping.Name))
                    {
                        // Override auto registrations.
                        assemblyTypeMap[fromType.AssemblyQualifiedName] = new List<AssemblyTypeMap> { typeMap };
                        continue;
                    }

                    assemblyTypeMap[fromType.AssemblyQualifiedName].Add(typeMap);
                }
                else
                {
                    assemblyTypeMap[fromType.AssemblyQualifiedName] = new List<AssemblyTypeMap> { typeMap };
                }
            }
        }

    }
}

