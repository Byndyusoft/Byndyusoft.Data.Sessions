using Byndyusoft.Data.Sessions.Example.Nh;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class NhServiceCollectionExtensions
    {
        public static IServiceCollection AddNhDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddNHibernateSessions();

            services.AddNhSession(
                config =>
                {
                    config
                        .Database(MsSqliteConfiguration.Standard.ConnectionString(x => x.Is(connectionString)))
                        .Mappings(m =>
                        {
                            m.FluentMappings.AddFromAssemblyOf<NhRepository>()
                                .Conventions.Add(DefaultLazy.Never());
                        });
                });

            services.Scan(scan =>
                scan.FromAssemblyOf<NhRepository>()
                    .AddClasses(classes => classes.AssignableTo<NhRepository>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime());

            return services;
        }
    }
}