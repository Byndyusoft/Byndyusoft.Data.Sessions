using CommunityToolkit.Diagnostics;
using System.Data;

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
        return CreateSession(IsolationLevel.Unspecified);
    }

    public virtual ISession CreateSession(IsolationLevel isolationLevel)
    {
        var session = new Session(_sessionStorage, isolationLevel);
        return StartCore<ISession>(session);
    }

    public ICommitableSession CreateCommitableSession()
    {
        return CreateCommitableSession(IsolationLevel.Unspecified);
    }

    public virtual ICommitableSession CreateCommitableSession(IsolationLevel isolationLevel)
    {
        var session = new Session(_sessionStorage, isolationLevel);
        return StartCore<ICommitableSession>(session);
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