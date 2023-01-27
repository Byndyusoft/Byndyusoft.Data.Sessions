using System.Diagnostics;
using System.Reflection;

namespace Byndyusoft.Data.Sessions;

/// <summary>
/// Options to configure Session's support for OpenTelemetry tracing.
/// Currently no options are available.
/// </summary>
public class SessionTracingOptions
{
    private static readonly AssemblyName AssemblyName = typeof(Session).Assembly.GetName();
    private static readonly string Version = AssemblyName.Version!.ToString();
    public static readonly string ActivitySourceName = AssemblyName.Name;

    internal static ActivitySource CreateActivitySource() => new(ActivitySourceName, Version);
}