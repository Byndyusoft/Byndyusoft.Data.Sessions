using Byndyusoft.Data.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Byndyusoft.Data.EntityFramework
{
    internal class EfSessionFactory<TContext> where TContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;

        public EfSessionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public EfSession<TContext> CreateSession(ISession session)
        {
            var context = _serviceProvider.GetRequiredService<TContext>();

            if (session is ICommitableSession)
                try
                {
                    context.Database.BeginTransaction();
                }
                catch
                {
                    context.Dispose();
                    throw;
                }

            return new EfSession<TContext>(context);
        }
    }
}