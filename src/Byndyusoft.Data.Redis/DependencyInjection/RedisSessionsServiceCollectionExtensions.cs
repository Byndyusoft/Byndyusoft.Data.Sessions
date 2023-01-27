using Byndyusoft.Data.Redis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisSessionsServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisSessions(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            services.TryAddSingleton<IRedisConnectionAccessor, RedisConnectionAccessor>();
            services.TryAddSingleton<RedisSessionFactory>();

            return services;
        }

        public static IServiceCollection AddRedisSessions(this IServiceCollection services, string configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddRedisSessions();

            services.AddSingleton(Options.Options.Create(ConfigurationOptions.Parse(configuration)));

            return services;
        }
    }
}