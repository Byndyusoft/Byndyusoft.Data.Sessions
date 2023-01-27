using System;
using System.Threading;

namespace Byndyusoft.Data.Sessions;

public class SessionStorage : ISessionStorage
{
    // ReSharper disable once InconsistentNaming
    private static readonly AsyncLocal<Wrapper?> _current = new();

    public ISession? GetCurrent() => _current.Value?.Session;

    public void SetCurrent(ISession? session)
    {
        var wrapper = _current.Value ??= new Wrapper();
        if (session != null && wrapper.Session != null)
            throw new InvalidOperationException($"{nameof(Session)} already exists");

        wrapper.Session = session;
    }
        
    private class Wrapper
    {
        public ISession? Session { get; set; }
    }
}