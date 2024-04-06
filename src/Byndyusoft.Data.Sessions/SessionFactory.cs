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
        var session = new Session(_sessionStorage);
        return StartCore<ISession>(session);
    }

    public virtual ISession CreateSession(IsolationLevel isolationLevel)
    {
        var session = new Session(_sessionStorage, isolationLevel);
        return StartCore<ISession>(session);
    }

    public ICommittableSession CreateCommittableSession()
    {
        return CreateCommittableSession(IsolationLevel.Unspecified);
    }

    public virtual ICommittableSession CreateCommittableSession(IsolationLevel isolationLevel)
    {
        var session = new Session(_sessionStorage, isolationLevel);
        return StartCore<ICommittableSession>(session);
    }

    private static T StartCore<T>(Session session)
        where T : ISession
    {
        try
        {
            session.Start();
            return (T)(ISession)session;
        }
        catch
        {
            session.Dispose();
            throw;
        }
    }
}