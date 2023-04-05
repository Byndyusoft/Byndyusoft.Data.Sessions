using Byndyusoft.Data.Sessions;
using StackExchange.Redis;

namespace Byndyusoft.Data.Redis
{
    internal class RedisConnectionAccessor : SessionConsumer, IRedisConnectionAccessor
    {
        private readonly string _key = nameof(RedisSession);
        private readonly RedisSessionFactory _redisSessionFactory;

        public RedisConnectionAccessor(ISessionAccessor sessionAccessor, RedisSessionFactory redisSessionFactory)
            : base(sessionAccessor)
        {
            _redisSessionFactory = redisSessionFactory;
        }

        public IConnectionMultiplexer Connection
        {
            get
            {
                var session = Session;
                var redisSession = session.GetOrEnlist(_key, () => _redisSessionFactory.CreateSession());
                return redisSession.Connection;
            }
        }
    }
}