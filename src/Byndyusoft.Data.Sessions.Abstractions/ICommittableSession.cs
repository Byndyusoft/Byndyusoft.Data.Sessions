using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions;

/// <summary>
///     Represents a Committable session.
/// </summary>
public interface ICommittableSession : ISession
{
    /// <summary>
    ///     Gets the isolation level for this <see cref="ISession" />.
    /// </summary>
    new IsolationLevel IsolationLevel { get; }

    /// <summary>
    ///     Asynchronously commits the <see cref="ICommittableSession" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     An optional token to cancel the asynchronous operation. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Asynchronously rolls back the <see cref="ICommittableSession" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     An optional token to cancel the asynchronous operation. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}