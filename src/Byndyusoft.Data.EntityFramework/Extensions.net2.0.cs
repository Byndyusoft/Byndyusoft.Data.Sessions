using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.EntityFramework
{
#if NETSTANDARD2_0
    internal static class Extensions
    {
        public static ValueTask DisposeAsync(this DbContext context)
        {
            context.Dispose();
            return new ValueTask();
        }

        public static ValueTask CommitTransactionAsync(this DatabaseFacade database, CancellationToken _)
        {
            database.CommitTransaction();
            return new ValueTask();
        }

        public static ValueTask RollbackTransactionAsync(this DatabaseFacade database, CancellationToken _)
        {
            database.RollbackTransaction();
            return new ValueTask();
        }
    }
#endif
}