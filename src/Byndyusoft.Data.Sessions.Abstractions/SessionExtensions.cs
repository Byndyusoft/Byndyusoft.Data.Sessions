using System;

namespace Byndyusoft.Data.Sessions;

public static class SessionExtensions
{
    /// <summary>
    ///     Gets or Enlists an dependentSession to participate in a <see cref="ISession"/>.
    /// </summary>
    /// <param name="session">The instance of <see cref="ISession"/>.</param>
    /// <param name="key">The <paramref name="key"/>.</param>
    /// <param name="func">The func for dependentSession.</param>
    public static T GetOrEnlist<T>(this ISession session, string key, Func<T> func)
        where T : IDependentSession
    {
        if (session.DependentSessions.TryGetValue(key, out var dependentSession) == false)
        {
            dependentSession = func();
            if (session.Enlist(key, dependentSession) == false)
            {
                dependentSession.Dispose();
                dependentSession = session.DependentSessions[key];
            }
        }
        return (T)dependentSession;
    }
}