using Byndyusoft.Data.Sessions;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Redis
{
    internal class RedisSession : IDependentSession, IDisposable
    {
        private readonly IRedisConnectionFactory _connectionFactory;
        private readonly object _lock = new object();
        private volatile IConnectionMultiplexer? _connection;

        public RedisSession(IRedisConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IConnectionMultiplexer Connection
        {
            get
            {
                if (_connection?.IsConnected != false)
                    lock (_lock)
                    {
                        if (_connection?.IsConnected != false)
                        {
                            _connection?.Dispose();
                            _connection = _connectionFactory.CreateConnection();
                        }
                    }

                return _connection!;
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _connection = null;

            GC.SuppressFinalize(this);
        }

        public ValueTask CommitAsync(CancellationToken _) => new ValueTask();

        public async ValueTask RollbackAsync(CancellationToken _) => new ValueTask();
    }
}