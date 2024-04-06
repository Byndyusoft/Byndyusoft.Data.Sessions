namespace Byndyusoft.Data.Sessions;

/// <summary>
///     A session that specifies the relative order it should run.
/// </summary>
public interface IOrderedDependentSession
{
    /// <summary>
    ///     Gets the order value for determining the order of committing of sessions.
    ///     Sessions commit in ascending numeric value of the Order property.
    /// </summary>
    public int Order { get; }
}