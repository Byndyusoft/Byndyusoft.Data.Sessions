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
                if (session.DependentSessions.TryGetValue(_key, out var nhSession) == false)
                    session.Enlist(_key, nhSession = _sessionFactory.Create(session), true);

                return ((NhSession) nhSession).Session!;
            }
        }
    }
}