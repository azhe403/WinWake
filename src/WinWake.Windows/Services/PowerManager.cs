using WinWake.Core.Services;
using WinWake.Windows.Native;

namespace WinWake.Windows.Services;

public class PowerManager : IPowerManager, IDisposable
{
    private readonly System.Threading.Timer _reassertTimer;
    private bool _isPreventingSleep;
    private bool _disposed;

    public PowerManager()
    {
        _reassertTimer = new System.Threading.Timer(ReassertExecutionState, null, Timeout.Infinite, Timeout.Infinite);
    }

    public void PreventSleep()
    {
        if (_isPreventingSleep) return;

        NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED);
        _reassertTimer.Change(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
        _isPreventingSleep = true;
    }

    public void RestoreSleep()
    {
        if (!_isPreventingSleep) return;

        _reassertTimer.Change(Timeout.Infinite, Timeout.Infinite);
        NativeMethods.SetThreadExecutionState(0);
        _isPreventingSleep = false;
    }

    private void ReassertExecutionState(object? state)
    {
        NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        RestoreSleep();
        _reassertTimer.Dispose();
        GC.SuppressFinalize(this);
    }
}
