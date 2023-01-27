using Byndyusoft.Data.NHibernate;

namespace Byndyusoft.Data.Sessions.Example.Nh
{
    public class NhRepository
    {
        private readonly INhSessionAccessor _sessionAccessor;

        public NhRepository(INhSessionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }

        protected global::NHibernate.ISession Session => _sessionAccessor.Session;
    }
}