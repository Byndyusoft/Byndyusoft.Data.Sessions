namespace Byndyusoft.Data.NHibernate
{
    public interface INhSessionAccessor
    {
        global::NHibernate.ISession Session { get; }
    }
}
