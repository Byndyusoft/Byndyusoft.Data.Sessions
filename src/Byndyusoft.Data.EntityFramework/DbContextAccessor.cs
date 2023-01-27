using Byndyusoft.Data.Sessions;
using Microsoft.EntityFrameworkCore;

namespace Byndyusoft.Data.EntityFramework
{
    internal class DbContextAccessor<TContext> : IDbContextAccessor<TContext> where TContext : DbContext
    {
        private static readonly string Key = $"ef-session_{typeof(TContext).Name}";
        private readonly EfSessionFactory<TContext> _efSessionFactory;
        private readonly ISessionAccessor _sessionAccessor;

        public DbContextAccessor(ISessionAccessor sessionAccessor, EfSessionFactory<TContext> efSessionFactory)
        {
            _sessionAccessor = sessionAccessor;
            _efSessionFactory = efSessionFactory;
        }

        public TContext Context
        {
            get
            {
                var session = _sessionAccessor.Session;
                if (session.DependentSessions.TryGetValue(Key, out var efSession) == false)
                    session.Enlist(Key, efSession = _efSessionFactory.CreateSession(session));

                return ((EfSession<TContext>)efSession).Context!;
            }
        }
    }
}