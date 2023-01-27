using Byndyusoft.Data.Sessions;
using System;

// ReSharper disable once CheckNamespace
namespace OpenTelemetry.Trace;

/// <summary>
/// Extension method for setting up DbSession OpenTelemetry tracing.
/// </summary>
public static class DataSessionTracerProviderBuilderExtensions
{
    /// <summary>
    /// Subscribes to the DbSession activity source to enable OpenTelemetry tracing.
    /// </summary>
    public static TracerProviderBuilder AddDataSessions(
        this TracerProviderBuilder builder,
        Action<SessionTracingOptions>? _ = null)
        => builder.AddSource(SessionTracingOptions.ActivitySourceName);
}