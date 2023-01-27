using StackExchange.Redis;

namespace Byndyusoft.Data.Redis
{
    public interface IRedisConnectionAccessor
    {
        IConnectionMultiplexer Connection { get; }
    }
}