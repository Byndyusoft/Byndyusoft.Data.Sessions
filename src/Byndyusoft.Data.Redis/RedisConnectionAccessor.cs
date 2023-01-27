using Byndyusoft.Data.Sessions;
using StackExchange.Redis;

namespace Byndyusoft.Data.Redis
{
    internal class RedisConnectionAccessor : IRedisConnectionAccessor
    {
        private readonly string _key = nameof(RedisSession);
        private readonly RedisSessionFactory _redisSessionFactory;
        private readonly ISessionAccessor _sessionAccessor;

        public RedisConnectionAccessor(ISessionAccessor sessionAccessor, RedisSessionFactory redisSessionFactory)
        {
            _sessionAccessor = sessionAccessor;
            _redisSessionFactory = redisSessionFactory;
        }

        public IConnectionMultiplexer Connection
        {
            get
            {
                var session = _sessionAccessor.Session;
                if (session.DependentSessions.TryGetValue(_key, out var redisSession) == false)
                    session.Enlist(_key, redisSession = _redisSessionFactory.CreateSession());

                return ((RedisSession)redisSession).Connection;
            }
        }
    }
}