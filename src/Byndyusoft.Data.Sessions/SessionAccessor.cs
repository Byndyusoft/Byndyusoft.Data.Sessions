using CommunityToolkit.Diagnostics;

namespace Byndyusoft.Data.Sessions;

public class SessionAccessor : ISessionAccessor
{
    private readonly ISessionStorage _sessionStorage;
        
    public SessionAccessor(ISessionStorage sessionStorage)
    {
        Guard.IsNotNull(sessionStorage, nameof(sessionStorage));

        _sessionStorage = sessionStorage;
    }

    public ISession? Session => _sessionStorage.GetCurrent();
}