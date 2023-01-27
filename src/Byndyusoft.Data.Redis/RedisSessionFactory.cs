namespace Byndyusoft.Data.Redis
{
    internal class RedisSessionFactory
    {
        private readonly IRedisConnectionFactory _connectionFactory;

        public RedisSessionFactory(IRedisConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public RedisSession CreateSession()
        {
            return new RedisSession(_connectionFactory);
        }
    }
}