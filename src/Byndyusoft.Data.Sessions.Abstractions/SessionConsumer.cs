namespace Byndyusoft.Data.Sessions;

using CommunityToolkit.Diagnostics;
using System;

/// <summary>
///     Abstract class that consumes <see cref="ISession" />.
/// </summary>
public abstract class SessionConsumer
{
    private readonly ISessionAccessor _sessionAccessor;

    /// <summary>
    ///     Initializes a new instance of <see cref="SessionConsumer" /> with specified <paramref name="sessionAccessor" />.
    /// </summary>
    /// <param name="sessionAccessor">An instance of <see cref="ISessionAccessor" />.</param>
    protected SessionConsumer(ISessionAccessor sessionAccessor)
    {
        Guard.IsNotNull(sessionAccessor, nameof(sessionAccessor));

        _sessionAccessor = sessionAccessor;
    }

    /// <summary>
    ///     Gets the current <see cref="ISession" />.
    /// </summary>
    protected ISession Session => _sessionAccessor.Session ??
                                      throw new InvalidOperationException(
                                          $"There is no current {nameof(Session)}");
}