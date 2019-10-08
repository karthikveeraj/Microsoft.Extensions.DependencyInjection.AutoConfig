namespace Microsoft.Extensions.DependencyInjection
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the json configuration section "AutoTypeResolveConfig".
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DependencyInjectionConfig
    {
        /// <summary>
        /// Gets or sets the register assemblies configuration.
        /// </summary>
        public Collection<NameType> RegisterAssemblies { get; } = new Collection<NameType>();

        /// <summary>
        /// Gets or sets the register types configuration.
        /// </summary>
        public Collection<TypeMap> RegisterTypes { get; } = new Collection<TypeMap>();
    }
}
