using Byndyusoft.Data.EntityFramework;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class EfSessionsServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkSessions(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton(typeof(IDbContextAccessor<>), typeof(DbContextAccessor<>));
            services.TryAddSingleton(typeof(EfSessionFactory<>));

            return services;
        }
    }
}