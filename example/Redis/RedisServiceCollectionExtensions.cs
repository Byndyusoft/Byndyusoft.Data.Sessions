

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddRedisSessions(connectionString);

            return services;
        }
    }
}