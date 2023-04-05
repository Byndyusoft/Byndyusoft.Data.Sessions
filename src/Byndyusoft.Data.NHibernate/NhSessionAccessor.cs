using Byndyusoft.Data.Sessions;

namespace Byndyusoft.Data.NHibernate
{
    internal class NhSessionAccessor : SessionConsumer, INhSessionAccessor
    {
        private readonly string _key = nameof(NhSession);
        private readonly NhSessionFactory _sessionFactory;

        public NhSessionAccessor(ISessionAccessor sessionAccessor, NhSessionFactory sessionFactory)
            : base(sessionAccessor)
        {
            _sessionFactory = sessionFactory;
        }

        public new global::NHibernate.ISession Session
        {
            get
            {
                var session = base.Session;
                var nhSession = session.GetOrEnlist(_key, () => _sessionFactory.Create(session));
                return nhSession.Session!;
            }
        }
    }
}