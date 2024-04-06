using CommunityToolkit.Diagnostics;
using IsolationLevel = System.Data.IsolationLevel;

namespace Byndyusoft.Data.Sessions;

public class SessionFactory : ISessionFactory
{
    private readonly ISessionStorage _sessionStorage;

    public SessionFactory(ISessionStorage sessionStorage)
    {
        Guard.IsNotNull(sessionStorage, nameof(sessionStorage));

        _sessionStorage = sessionStorage;
    }

    public virtual ISession CreateSession()
    {
        return CreateSessionCore(null);
    }

    public virtual ISession CreateSession(IsolationLevel isolationLevel)
    {
        return CreateSessionCore(isolationLevel);
    }

    public ICommittableSession CreateCommittableSession()
    {
        return CreateCommittableSession(IsolationLevel.Unspecified);
    }

    public virtual ICommittableSession CreateCommittableSession(IsolationLevel isolationLevel)
    {
        var session = new CommittableSession(_sessionStorage, isolationLevel);
        try
        {
            session.Start();
            return session;
        }
        catch
        {
            session.Dispose();
            throw;
        }
    }

    public virtual ISession CreateSessionCore(IsolationLevel? isolationLevel)
    {
        var session = new Session(_sessionStorage, isolationLevel);
        try
        {
            session.Start();
            return session;
        }
        catch
        {
            session.Dispose();
            throw;
        }
    }
}