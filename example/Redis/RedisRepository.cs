using Byndyusoft.Data.Redis;
using StackExchange.Redis;

namespace Byndyusoft.Data.Sessions.Example.Redis
{
    public abstract class RedisRepository
    {
        private readonly IRedisConnectionAccessor _sessionAccessor;

        protected RedisRepository(IRedisConnectionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }

        protected IDatabase Db => _sessionAccessor.Connection.GetDatabase();
    }
}