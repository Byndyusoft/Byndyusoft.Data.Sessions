using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions;

internal class DependentSessions : ConcurrentDictionary<string, IDependentSession>, IDisposable, IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        foreach (var value in Values)
            switch (value)
            {
                case IAsyncDisposable asyncDisposable:
                    await asyncDisposable.DisposeAsync().ConfigureAwait(false);
                    break;
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }

        Clear();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        foreach (var session in Values)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                await session.CommitAsync(cts.Token).ConfigureAwait(false);
            }
            catch
            {
                cts.Cancel();
                throw;
            }
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        foreach (var session in Values)
            await session.RollbackAsync(cancellationToken).ConfigureAwait(false);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        foreach (var value in Values)
            value.Dispose();

        Clear();
    }
}