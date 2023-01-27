using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions.Example.Dapper
{
    public class DapperSession : IDependentSession, IAsyncDisposable
    {
        public DbConnection? Connection { get; private set; }
        public DbTransaction? Transaction { get; private set; }

        public DapperSession(DbConnection connection, DbTransaction? transaction = null)
        {
            Connection = connection;
            Transaction = transaction;
        }
        public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction is null)
                return;

            await Transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        public async ValueTask RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction is null)
                return;

            await Transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync()
        {
            if (Transaction is not null)
            {
                await Transaction.DisposeAsync().ConfigureAwait(false);
                Transaction = null;
            }

            if (Connection is not null)
            {
                await Connection.DisposeAsync().ConfigureAwait(false);
                Connection = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}