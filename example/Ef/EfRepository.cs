using Byndyusoft.Data.EntityFramework;

namespace Byndyusoft.Data.Sessions.Example.Ef
{
    public class EfRepository
    {
        private readonly IDbContextAccessor<EfDbContext> _contextAccessor;

        public EfRepository(IDbContextAccessor<EfDbContext> contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected EfDbContext DbContext => _contextAccessor.Context;
    }
}