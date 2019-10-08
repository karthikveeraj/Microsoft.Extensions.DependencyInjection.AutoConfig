namespace Microsoft.Extensions.DependencyInjection
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the json configuration section "AutoTypeResolveConfig/registerAssemblies" individual entities.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NameType
    {
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the dependency resolution (Transient/Singleton/Scoped).
        /// </summary>
        public ServiceLifetime Type { get; set; }

        /// <summary>
        /// Gets or sets the order of the dependency resolution.
        /// </summary>
        public short Order { get; set; } 
    }
}
