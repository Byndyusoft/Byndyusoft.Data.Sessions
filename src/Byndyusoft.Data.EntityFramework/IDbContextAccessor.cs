using Microsoft.EntityFrameworkCore;

namespace Byndyusoft.Data.EntityFramework
{
    public interface IDbContextAccessor<out TContext> where TContext : DbContext
    {
        TContext Context { get; }
    }
}