using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Byndyusoft.Data.Sessions.Example.Dapper
{
    public class DapperPeopleRepository : IPeopleRepository
    {
        private readonly DapperSessionAccessor _sessionAccessor;

        public DapperPeopleRepository(DapperSessionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }

        public async IAsyncEnumerable<People> ListAll([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var session = _sessionAccessor.Session;

            var peoples = await session.Connection.QueryAsync<People>("SELECT id, name FROM peoples", transaction: session.Transaction);
            foreach (var people in peoples)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return people;
            }
        }

        public async Task AddAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var session = _sessionAccessor.Session;

            await session.Connection.ExecuteAsync("INSERT INTO peoples(id, name) VALUES (@id, @name)", new {id, name},
                transaction: session.Transaction);
        }
    }
}