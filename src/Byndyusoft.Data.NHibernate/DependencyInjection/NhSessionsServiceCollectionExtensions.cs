// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    using Byndyusoft.Data.NHibernate;
    using Extensions;
    using FluentNHibernate.Cfg;
    using NHibernate;
    using System;

    public static class NhSessionsServiceCollectionExtensions
    {
        public static IServiceCollection AddNHibernateSessions(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<NhSessionFactory>();
            services.TryAddSingleton<INhSessionAccessor, NhSessionAccessor>();
            services.TryAddSingleton<INhStatelessSessionAccessor, NhStatelessSessionAccessor>();

            return services;
        }

        public static IServiceCollection AddNhSession(this IServiceCollection services,
            Action<FluentConfiguration> optionsActions)
        {
            var options = Fluently.Configure();
            optionsActions(options);
            services.TryAddTransient(_ => options.BuildSessionFactory());
            services.TryAddTransient(s => s.GetRequiredService<ISessionFactory>().OpenSession());
            services.TryAddTransient(s => s.GetRequiredService<ISessionFactory>().OpenStatelessSession());
            return services;
        }
    }
}