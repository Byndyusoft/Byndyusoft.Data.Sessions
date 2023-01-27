using Byndyusoft.Data.Sessions;
using System;

namespace Byndyusoft.Data.NHibernate
{
    internal class NhSessionAccessor : INhSessionAccessor
    {
        private readonly string _key = nameof(NhSession);
        private readonly ISessionAccessor _sessionAccessor;
        private readonly NhSessionFactory _sessionFactory;

        public NhSessionAccessor(ISessionAccessor sessionAccessor, NhSessionFactory sessionFactory)
        {
            _sessionAccessor = sessionAccessor;
            _sessionFactory = sessionFactory;
        }

        public global::NHibernate.ISession Session
        {
            get
            {
                var session = _sessionAccessor.Session;
                if (session.DependentSessions.TryGetValue(_key, out var nhSession) == false)
                    session.Enlist(_key, nhSession = _sessionFactory.Create(session));

                return ((NhSession) nhSession).Session!;
            }
        }
    }
}