using System;
using Byndyusoft.Data.Redis;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Bytecode;

namespace Byndyusoft.Data.Sessions.Example.Redis
{
    public class RedisPeopleRepository : RedisRepository, IPeopleRepository
    {
        public RedisPeopleRepository(IRedisConnectionAccessor sessionAccessor) : base(sessionAccessor)
        {
        }

        public async IAsyncEnumerable<People> ListAll([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var values = await Db.ListRangeAsync("people_*");
            foreach (var value in values)
            {
                yield return JsonSerializer.Deserialize<People>(value)!;
            }
        }

        public Task AddAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}