using NHibernate;

namespace Byndyusoft.Data.NHibernate
{
    public interface INhStatelessSessionAccessor
    {
        IStatelessSession Session { get; }
    }
}