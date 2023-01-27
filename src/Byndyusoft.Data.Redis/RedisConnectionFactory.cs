using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Byndyusoft.Data.Redis
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly ConfigurationOptions _options;

        public RedisConnectionFactory(IOptions<ConfigurationOptions> options)
        {
            _options = options.Value;
        }

        public IConnectionMultiplexer CreateConnection()
        {
            return ConnectionMultiplexer.Connect(_options);
        }
    }
}