namespace Byndyusoft.Data.Sessions.Internals;

internal static class DependentSessionExtensions
{
    public static int GetOrder(this IDependentSession session)
    {
        if (session is IOrderedDependentSession ordered)
            return ordered.Order;
        return 0;
    }
}