using StackExchange.Redis;

namespace Byndyusoft.Data.Redis
{
    public interface IRedisConnectionFactory
    {
        IConnectionMultiplexer CreateConnection();
    }
}