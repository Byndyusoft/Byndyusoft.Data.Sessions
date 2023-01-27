using System;

namespace Byndyusoft.Data.Sessions.Example.Dapper
{
    public class DapperSessionAccessor
    {
        private readonly string _key = nameof(DapperSession);
        private readonly ISessionAccessor _sessionAccessor;
        private readonly DapperSessionFactory _sessionFactory;

        public DapperSessionAccessor(ISessionAccessor sessionAccessor, DapperSessionFactory sessionFactory)
        {
            _sessionAccessor = sessionAccessor;
            _sessionFactory = sessionFactory;
        }

        public DapperSession Session
        {
            get
            {
                var session = _sessionAccessor.Session;
                if (session.DependentSessions.TryGetValue(_key, out var dapperSession) == false)
                    session.Enlist(_key, dapperSession = _sessionFactory.Create(session));

                return (DapperSession)dapperSession ?? throw new InvalidOperationException("No current session");
            }
        }
    }
}