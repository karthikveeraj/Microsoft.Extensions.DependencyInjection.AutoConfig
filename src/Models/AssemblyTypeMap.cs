namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the mapping information of assembly, its type and type of service life time.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AssemblyTypeMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyTypeMap"/> class.
        /// </summary>
        /// <param name="assemblyType">Type of the assembly.</param>
        /// <param name="lifetimeType">Type of the lifetime.</param>
        /// <param name="name">The name.</param>
        public AssemblyTypeMap(Type assemblyType, ServiceLifetime lifetimeType, string name)
        {
            AssemblyType = assemblyType;
            Type = lifetimeType;
            Name = name;
        }

        /// <summary>
        /// Gets or sets the type of the assembly.
        /// </summary>
        public Type AssemblyType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ServiceLifetime"/>.
        /// </summary>
        public ServiceLifetime Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
