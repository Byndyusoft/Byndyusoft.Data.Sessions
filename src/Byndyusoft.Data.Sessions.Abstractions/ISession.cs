using System;
using System.Collections.Generic;
using System.Data;

namespace Byndyusoft.Data.Sessions;

/// <summary>
///     Represents a session.
/// </summary>
public interface ISession : IDisposable, IAsyncDisposable
{
    /// <summary>
    ///     Gets the isolation level for this <see cref="ISession" />.
    /// </summary>
    IsolationLevel IsolationLevel { get; }

    /// <summary>
    ///     Gets a collection of key/value pairs that provide additional information about the <see cref="ISession" />.
    /// </summary>
    IDictionary<string, object> Items { get; }
    
    /// <summary>
    ///     Gets a key/value collection that can be used to organize items inside <see cref="ISession"/>.
    /// </summary>
    IReadOnlyDictionary<string, IDependentSession> DependentSessions { get; }

    /// <summary>
    /// Enlists an dependentSession to participate in a <see cref="ISession"/>.
    /// </summary>
    /// <param name="key">The <paramref name="key"/>.</param>
    /// <param name="dependentSession">The <paramref name="dependentSession"/>.</param>
    bool Enlist(string key, IDependentSession dependentSession);
}