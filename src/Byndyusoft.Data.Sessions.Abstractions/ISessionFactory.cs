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
    ///     Returns a new instance of the commitable  session's class that implements the <see cref="ICommitableSession" /> interface.
    /// </summary>
    /// <returns>A new instance of <see cref="ICommitableSession" />.</returns>
    ICommitableSession CreateCommitableSession();

    /// <summary>
    ///     Returns a new instance of the commitable  session's class that implements the <see cref="ICommitableSession" /> interface
    ///     with specified <see cref="IsolationLevel" />.
    /// </summary>
    /// <param name="isolationLevel">
    ///     One of the enumeration values that specifies the isolation level for the transaction to
    ///     use.
    /// </param>
    /// <returns>A new instance of <see cref="ICommitableSession" />.</returns>
    ICommitableSession CreateCommitableSession(IsolationLevel isolationLevel);
}