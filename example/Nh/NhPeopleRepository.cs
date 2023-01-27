using Byndyusoft.Data.NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions.Example.Nh
{
    public class NhPeopleRepository : NhRepository, IPeopleRepository
    {
        public NhPeopleRepository(INhSessionAccessor sessionAccessor) : base(sessionAccessor)
        {
        }

        public IAsyncEnumerable<People> ListAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Session.Query<People>().ToAsyncEnumerable();
        }

        public async Task AddAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            await Session.SaveAsync(new People {Name = name}, cancellationToken);
        }
    }
}