namespace Microsoft.Extensions.DependencyInjection
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides the configuration of interfaces and their implementors along with the way of dependency resolution.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TypeMap
    {
        /// <summary>
        /// Gets or sets the interface type information.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the interface implementer/instance provider type information.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the type of the dependency resolution (Transient/Singleton/Scoped).
        /// </summary>
        public ServiceLifetime Type { get; set; }

        /// <summary>
        /// Gets or sets the name. This is used to differentiate the multiple implementation for the same type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the order. This helps to order the type registrations.
        /// </summary>
        public short Order { get; set; }
    }
}
