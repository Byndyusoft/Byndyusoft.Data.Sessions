namespace Byndyusoft.Data.Sessions;

public abstract class SessionConsumer
{
    private readonly ISessionAccessor _sessionAccessor;

    protected SessionConsumer(ISessionAccessor sessionAccessor)
    {
        _sessionAccessor = sessionAccessor;
    }

    protected ISession Session => _sessionAccessor.Session;
}