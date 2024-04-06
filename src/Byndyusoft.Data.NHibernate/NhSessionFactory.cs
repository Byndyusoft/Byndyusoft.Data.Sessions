using Byndyusoft.Data.Sessions;

namespace Byndyusoft.Data.NHibernate
{

    internal class NhSessionFactory
    {
        private readonly global::NHibernate.ISessionFactory _sessionFactory;

        public NhSessionFactory(global::NHibernate.ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public NhSession Create(ISession session)
        {
            var nhSession = _sessionFactory.OpenSession();

            if (session is not ICommittableSession Committable)
                return new NhSession(nhSession, null);

            try
            {
                var nhTransaction = nhSession.BeginTransaction(Committable.IsolationLevel);
                return new NhSession(nhSession, nhTransaction);
            }
            catch
            {
                nhSession.Dispose();
                throw;
            }
        }

        public NhStatelessSession CreateStatelessSession(ISession session)
        {
            var nhSession = _sessionFactory.OpenStatelessSession();

            if (session is not ICommittableSession Committable)
                return new NhStatelessSession(nhSession, null);

            try
            {
                var nhTransaction = nhSession.BeginTransaction(Committable.IsolationLevel);
                return new NhStatelessSession(nhSession, nhTransaction);
            }
            catch
            {
                nhSession.Dispose();
                throw;
            }
        }
    }
}