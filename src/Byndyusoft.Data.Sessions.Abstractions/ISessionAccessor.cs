using System;

namespace Byndyusoft.Data.Sessions;

/// <summary>
///     Provides access to the current <see cref="ISession" />, if one is available.
/// </summary>
public interface ISessionAccessor
{
    /// <summary>
    ///     Gets the current <see cref="ISession" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">If there is no active <see cref="ISession" /></exception>
    ISession? Session { get; }
}