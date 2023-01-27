using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions;

/// <summary>
///     Represents a commitable session.
/// </summary>
public interface ICommitableSession : ISession
{
    /// <summary>
    ///     Asynchronously commits the <see cref="ICommitableSession" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     An optional token to cancel the asynchronous operation. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Asynchronously rolls back the <see cref="ICommitableSession" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     An optional token to cancel the asynchronous operation. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}