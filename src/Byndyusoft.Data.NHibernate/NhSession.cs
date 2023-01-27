using Byndyusoft.Data.Sessions;
using NHibernate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.NHibernate
{
    internal class NhSession : IDependentSession, IDisposable
    {
        private ITransaction? _transaction;
        private global::NHibernate.ISession? _session;

        public NhSession(global::NHibernate.ISession session, ITransaction? transaction)
        {
            _session = session;
            _transaction = transaction;
        }

        internal global::NHibernate.ISession? Session => _session;

        public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null || _session == null)
                return;

            await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            await _session.FlushAsync(cancellationToken).ConfigureAwait(false);
        }

        public async ValueTask RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null || _session == null)
                return;

            await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            await _session.FlushAsync(cancellationToken).ConfigureAwait(false);
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