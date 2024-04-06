using System.Data;

namespace Byndyusoft.Data.Sessions;

/// <summary>
///     Represents a set of methods for creating instances of <see cref="ISession"/>.
/// </summary>
public interface ISessionFactory
{
    /// <summary>
    ///     Returns a new instance of the session's class that implements the <see cref="ISession" /> interface.
    /// </summary>
    /// <returns>A new instance of <see cref="ISession" />.</returns>
    ISession CreateSession();

    /// <summary>
    ///     Returns a new instance of the session's class that implements the <see cref="ISession" /> interface
    ///     with specified <see cref="IsolationLevel" />.
    /// </summary>
    /// <param name="isolationLevel">
    ///     One of the enumeration values that specifies the isolation level for the transaction to
    ///     use.
    /// </param>
    /// <returns>A new instance of <see cref="ISession" />.</returns>
    ISession CreateSession(IsolationLevel isolationLevel);


    /// <summary>
    ///     Returns a new instance of the Committable  session's class that implements the <see cref="ICommittableSession" /> interface.
    /// </summary>
    /// <returns>A new instance of <see cref="ICommittableSession" />.</returns>
    ICommittableSession CreateCommittableSession();

    /// <summary>
    ///     Returns a new instance of the Committable  session's class that implements the <see cref="ICommittableSession" /> interface
    ///     with specified <see cref="IsolationLevel" />.
    /// </summary>
    /// <param name="isolationLevel">
    ///     One of the enumeration values that specifies the isolation level for the transaction to
    ///     use.
    /// </param>
    /// <returns>A new instance of <see cref="ICommittableSession" />.</returns>
    ICommittableSession CreateCommittableSession(IsolationLevel isolationLevel);
}