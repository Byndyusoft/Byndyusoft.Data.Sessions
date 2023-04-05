using Byndyusoft.Data.Sessions;
using NHibernate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.NHibernate
{
    internal class NhStatelessSession : IDependentSession
    {
        private ITransaction? _transaction;
        private IStatelessSession? _session;

        public NhStatelessSession(IStatelessSession session, ITransaction? transaction)
        {
            _session = session;
            _transaction = transaction;
        }

        internal IStatelessSession? Session => _session;

        public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null || _session == null)
                return;

            await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        public async ValueTask RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null || _session == null)
                return;

            await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _session?.Dispose();
            _session = null;

            GC.SuppressFinalize(this);
        }
    }
}