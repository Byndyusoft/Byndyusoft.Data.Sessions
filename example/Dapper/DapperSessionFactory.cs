using System.Data.Common;

namespace Byndyusoft.Data.Sessions.Example.Dapper
{
    public class DapperSessionFactory
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        public DapperSessionFactory(DbProviderFactory providerFactory, string connectionString)
        {
            _providerFactory = providerFactory;
            _connectionString = connectionString;
        }

        public DapperSession Create(ISession session)
        {
            var connection = _providerFactory.CreateConnection()!;
            connection.ConnectionString = _connectionString;
            connection.Open();
            
            if (session is not ICommittableSession committable)
                return new DapperSession(connection);

            try
            {
                var transaction = connection.BeginTransaction(committable.IsolationLevel);
                return new DapperSession(connection, transaction);
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }
    }
}