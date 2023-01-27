using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions;

/// <summary>
///     Represents a dependent session.
/// </summary>
public interface IDependentSession
{
    /// <summary>
    ///     Asynchronously commits the <see cref="IDependentSession" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     An optional token to cancel the asynchronous operation. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="ValueTask" /> representing the asynchronous operation.</returns>
    ValueTask CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Asynchronously rolls back the <see cref="IDependentSession" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     An optional token to cancel the asynchronous operation. The default value is
    ///     <see cref="CancellationToken.None" />.
    /// </param>
    /// <returns>A <see cref="ValueTask" /> representing the asynchronous operation.</returns>
    ValueTask RollbackAsync(CancellationToken cancellationToken = default);
}