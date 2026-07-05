using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pt.Okx.Sdk.Storage
{
    /// <summary>
    /// Provides extension methods for registering <see cref="IStoragePathProvider"/>
    /// implementations in the dependency injection container.
    /// </summary>
    public static class StoragePathProviderServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a custom <see cref="IStoragePathProvider"/> implementation as a singleton,
        /// replacing any existing registration.
        /// </summary>
        /// <typeparam name="TProvider">
        /// The provider type implementing <see cref="IStoragePathProvider"/>.
        /// </typeparam>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection UseStoragePathProvider<TProvider>(this IServiceCollection services)
            where TProvider : class, IStoragePathProvider
        {
            services.Replace(ServiceDescriptor.Singleton<IStoragePathProvider, TProvider>());
            return services;
        }

        /// <summary>
        /// Registers a specific <see cref="IStoragePathProvider"/> instance as a singleton,
        /// replacing any existing registration.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="provider">The provider instance to register.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection UseStoragePathProvider(this IServiceCollection services, IStoragePathProvider provider)
        {
            services.Replace(ServiceDescriptor.Singleton(provider));
            return services;
        }
    }
}
