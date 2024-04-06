using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Sessions;

public class CommittableSession : Session, ICommittableSession
{
    private bool _completed;

    protected internal CommittableSession(
        ISessionStorage sessionStorage,
        IsolationLevel isolationLevel) 
            : base(sessionStorage, isolationLevel)
    {
    }

    public new IsolationLevel IsolationLevel => base.IsolationLevel ?? IsolationLevel.Unspecified;

    public virtual async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (_completed)
            return;

        _activity?.AddEvent(new ActivityEvent(SessionEvents.Committing));

        await _dependentSessions.CommitAsync(cancellationToken)
            .ConfigureAwait(false);

        _completed = true;
        _activity?.AddEvent(new ActivityEvent(SessionEvents.Commited));
    }

    public virtual async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (_completed)
            return;

        _activity?.AddEvent(new ActivityEvent(SessionEvents.RollingBack));

        await _dependentSessions.RollbackAsync(cancellationToken)
            .ConfigureAwait(false);

        _completed = true;
        _activity?.AddEvent(new ActivityEvent(SessionEvents.RolledBack));
    }
}