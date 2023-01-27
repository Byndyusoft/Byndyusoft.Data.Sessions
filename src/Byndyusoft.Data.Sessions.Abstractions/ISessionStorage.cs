namespace Byndyusoft.Data.Sessions;

public interface ISessionStorage
{
    ISession? GetCurrent();

    void SetCurrent(ISession? session);
}