using Byndyusoft.Data.Sessions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class SessionsServiceCollectionExtensions
{
    public static IServiceCollection AddDataSessions(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.TryAddSingleton<ISessionAccessor, SessionAccessor>();
        services.TryAddSingleton<ISessionFactory, SessionFactory>();
        services.TryAddSingleton<ISessionStorage, AsyncLocalSessionStorage>();

        return services;
    }
}