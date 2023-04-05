using Byndyusoft.Data.Sessions;

namespace Byndyusoft.Data.NHibernate
{
    internal class NhStatelessSessionAccessor : SessionConsumer, INhStatelessSessionAccessor
    {
        private readonly string _key = nameof(NhStatelessSession);
        private readonly NhSessionFactory _sessionFactory;

        public NhStatelessSessionAccessor(ISessionAccessor sessionAccessor, NhSessionFactory sessionFactory)
            : base(sessionAccessor)
        {
            _sessionFactory = sessionFactory;
        }

        public new global::NHibernate.IStatelessSession Session
        {
            get
            {
                var session = base.Session;
                var nhSession = session.GetOrEnlist(_key, () => _sessionFactory.CreateStatelessSession(session));
                return nhSession.Session!;
            }
        }
    }
}
