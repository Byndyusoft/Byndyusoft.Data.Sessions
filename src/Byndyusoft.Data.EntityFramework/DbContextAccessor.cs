using Byndyusoft.Data.Sessions;
using Microsoft.EntityFrameworkCore;

namespace Byndyusoft.Data.EntityFramework
{
    internal class DbContextAccessor<TContext> : SessionConsumer, IDbContextAccessor<TContext> where TContext : DbContext
    {
        private static readonly string Key = $"ef-session_{typeof(TContext).Name}";
        private readonly EfSessionFactory<TContext> _efSessionFactory;

        public DbContextAccessor(ISessionAccessor sessionAccessor, EfSessionFactory<TContext> efSessionFactory)
            : base(sessionAccessor)
        {
            _efSessionFactory = efSessionFactory;
        }

        public TContext Context
        {
            get
            {
                var session = Session;
                if (session.DependentSessions.TryGetValue(Key, out var efSession) == false)
                    session.Enlist(Key, efSession = _efSessionFactory.CreateSession(session), true);

                return ((EfSession<TContext>)efSession).Context!;
            }
        }
    }
}