namespace Microsoft.Extensions.DependencyInjection
{
    using System;

    /// <summary>
    /// Represents the registered type information in a container.
    /// </summary>
    public interface ITypeRegistration
    {
        /// <summary>
        /// Gets the <see cref="Type"/> information of the registered service.
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// Gets the registered name. Contains null value if you haven't registered with name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the implementation type for the registered type.
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// Gets the life time of the registered type.
        /// </summary>
        ServiceLifetime LifeTime { get; }
    }
}
