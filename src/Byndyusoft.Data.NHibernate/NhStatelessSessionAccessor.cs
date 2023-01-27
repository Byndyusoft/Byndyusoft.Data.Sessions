using Byndyusoft.Data.Sessions;
using System;

namespace Byndyusoft.Data.NHibernate
{
    internal class NhStatelessSessionAccessor : INhStatelessSessionAccessor
    {
        private readonly string _key = nameof(NhStatelessSession);
        private readonly ISessionAccessor _sessionAccessor;
        private readonly NhSessionFactory _sessionFactory;

        public NhStatelessSessionAccessor(ISessionAccessor sessionAccessor, NhSessionFactory sessionFactory)
        {
            _sessionAccessor = sessionAccessor;
            _sessionFactory = sessionFactory;
        }

        public global::NHibernate.IStatelessSession Session
        {
            get
            {
                ISession session = _sessionAccessor.Session;
                if (session.DependentSessions.TryGetValue(_key, out var nhSession) == false)
                    session.Enlist(_key, nhSession = _sessionFactory.CreateStatelessSession(session));

                return ((NhStatelessSession) nhSession).Session!;
            }
        }
    }
}

