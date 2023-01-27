using Byndyusoft.Data.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions.Example.Ef
{
    public class EfPeopleRepository : EfRepository, IPeopleRepository
    {
        public EfPeopleRepository(IDbContextAccessor<EfDbContext> contextAccessor) : base(contextAccessor)
        {
        }

        public IAsyncEnumerable<People> ListAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return DbContext.Peoples.ToAsyncEnumerable();
        }

        public async Task AddAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            await DbContext.Peoples.AddAsync(new People {Id = id, Name = name}, cancellationToken);
        }
    }
}