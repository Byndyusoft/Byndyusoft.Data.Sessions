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
                if (session.DependentSessions.TryGetValue(_key, out var redisSession) == false)
                    session.Enlist(_key, redisSession = _redisSessionFactory.CreateSession(), true);

                return ((RedisSession)redisSession).Connection;
            }
        }
    }
}