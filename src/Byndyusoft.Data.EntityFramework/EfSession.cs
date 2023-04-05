using Byndyusoft.Data.Sessions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.EntityFramework
{
    internal class EfSession<TContext> : IDependentSession, IAsyncDisposable where TContext : DbContext
    {
        public EfSession(TContext context)
        {
            Context = context;
        }

        public TContext? Context { get; private set; }

        public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
        {
            if (Context == null)
                return;

            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await Context.Database.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
        }

        public async ValueTask RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (Context == null)
                return;

            await Context.Database.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync()
        {
            if (Context != null)
            {
                await Context.DisposeAsync().ConfigureAwait(false);
                Context = null;

                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
            Context = null;

            GC.SuppressFinalize(this);
        }
    }
}