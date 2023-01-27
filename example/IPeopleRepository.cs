using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions.Example
{
    public interface IPeopleRepository
    {
        IAsyncEnumerable<People> ListAll(CancellationToken cancellationToken = default);

        Task AddAsync(int id, string name, CancellationToken cancellationToken = default);
    }
}