namespace Byndyusoft.Data.Sessions;

public static class SessionEvents
{
    public static readonly string Started = nameof(Started);
    public static readonly string Committing = nameof(Committing);
    public static readonly string Commited = nameof(Commited);
    public static readonly string RollingBack = nameof(RollingBack);
    public static readonly string RolledBack = nameof(RolledBack);
    public static readonly string Finishing = nameof(Finishing);
    public static readonly string Finished = nameof(Finished);
}