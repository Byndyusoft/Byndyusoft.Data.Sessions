using CommunityToolkit.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions;

public class Session : ISession
{
    private static readonly ActivitySource ActivitySource = SessionTracingOptions.CreateActivitySource();

    private bool _disposed;
    internal DependentSessions _dependentSessions = new ();
    private ConcurrentDictionary<string,object>? _items;
    protected Activity? _activity;
    private readonly IsolationLevel? _isolationLevel;
    private readonly ISessionStorage _sessionStorage = default!;

    protected Session()
    {
    }

    protected internal Session(
        ISessionStorage sessionStorage,
        IsolationLevel? isolationLevel = null) : this()
    {
        Guard.IsNotNull(sessionStorage, nameof(sessionStorage));

        _sessionStorage = sessionStorage;
        _isolationLevel = isolationLevel;
    }

    public ValueTask DisposeAsync()
    {
        if (_disposed)
            return new ValueTask();

        _disposed = true;
        _sessionStorage.SetCurrent(null);

        GC.SuppressFinalize(this);
        return DisposeAsyncCore();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _sessionStorage.SetCurrent(null);

        GC.SuppressFinalize(this);
        DisposeCore();
    }

    public IsolationLevel? IsolationLevel => _isolationLevel;

    public IDictionary<string, object> Items => _items ??= new ConcurrentDictionary<string, object>();

    public IReadOnlyDictionary<string, IDependentSession> DependentSessions
    {
        get
        {
            ThrowIfDisposed();
            return _dependentSessions;
        }
    }

    public void Start()
    {
        ThrowIfDisposed();

        _sessionStorage.SetCurrent(this);

        _activity = ActivitySource.StartActivity(nameof(Session));
        _activity?.SetTag("isolationlevel", _isolationLevel);
        _activity?.AddEvent(new ActivityEvent(SessionEvents.Started));
    }

    public bool Enlist(string key, IDependentSession dependentSession)
    {
        return _dependentSessions.TryAdd(key, dependentSession);
    }

    private async ValueTask DisposeAsyncCore()
    {
        await _dependentSessions.DisposeAsync()
            .ConfigureAwait(false);

        DisposeCore();
    }

    private void DisposeCore()
    {
        _dependentSessions.Dispose();
        _dependentSessions = null!;

        _activity?.Dispose();
        _activity = null;
    }

    protected void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().FullName);
    }
}