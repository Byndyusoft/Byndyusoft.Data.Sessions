using Byndyusoft.Data.Sessions.Example.Ef;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class EfServiceCollectionExtensions
    {
        public static IServiceCollection AddEfDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkSessions();
            services.AddEntityFrameworkSqlite();

            services.AddDbContext<EfDbContext>(
                options => { options.UseSqlite(connectionString); }, ServiceLifetime.Transient);

            services.Scan(scan =>
                scan.FromAssemblyOf<EfRepository>()
                    .AddClasses(classes => classes.AssignableTo<EfRepository>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime());

            return services;
        }
    }
}