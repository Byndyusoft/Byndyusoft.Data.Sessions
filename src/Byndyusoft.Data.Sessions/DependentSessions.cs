using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Sessions.Internals;

namespace Byndyusoft.Data.Sessions;

internal class DependentSessions : ConcurrentDictionary<string, IDependentSession>, IDisposable, IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        foreach (var value in Sessions)
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
        foreach (var session in OrderedSessions)
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
        foreach (var session in OrderedSessions)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                await session.RollbackAsync(cts.Token).ConfigureAwait(false);
            }
            catch
            {
                cts.Cancel();
                throw;
            }
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        foreach (var value in Sessions)
            value.Dispose();

        Clear();
    }
    
    private IEnumerable<IDependentSession> Sessions => Values;

    private IEnumerable<IDependentSession> OrderedSessions =>
        Sessions.OrderBy(session => session.GetOrder());
}