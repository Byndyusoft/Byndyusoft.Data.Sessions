using System;

namespace Byndyusoft.Data.Sessions;

public class SessionAccessor : ISessionAccessor
{
    private readonly ISessionStorage _sessionStorage;
        
    public SessionAccessor(ISessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public ISession Session => _sessionStorage.GetCurrent()  ?? throw new InvalidOperationException("There is no current Session");
}