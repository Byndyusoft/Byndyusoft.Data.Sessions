using System.Data.Common;
using Byndyusoft.Data.Sessions.Example.Dapper;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DapperServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperDataAccess(this IServiceCollection services, DbProviderFactory providerFactory, string connectionString)
        {
            services.TryAddSingleton<DapperPeopleRepository>();
            services.TryAddSingleton<DapperSessionAccessor>();
            services.TryAddSingleton(_ => new DapperSessionFactory(providerFactory, connectionString));

            return services;
        }
    }
}