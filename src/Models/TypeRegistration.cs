namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the registered type information in a container.
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.DependencyInjection.ITypeRegistration" />
    [ExcludeFromCodeCoverage]
    public class TypeRegistration : ITypeRegistration
    {
        /// <summary>
        /// Gets or sets the <see cref="Type" /> information of the registered type.
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the registered name. Contains null value if you haven't registered with name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the implementation type for the registered type.
        /// </summary>
        public Type ImplementationType { get; set; }

        /// <summary>
        /// Gets or sets the life time of the registered type.
        /// </summary>
        public ServiceLifetime LifeTime { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"From={ServiceType.Name};To={ImplementationType.Name};Name={Name};Type={LifeTime}";
        }
    }
}
