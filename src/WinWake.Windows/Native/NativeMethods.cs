using System.Runtime.InteropServices;

namespace WinWake.Windows.Native;

internal static class NativeMethods
{
    internal const uint ES_CONTINUOUS = 0x80000000;
    internal const uint ES_SYSTEM_REQUIRED = 0x00000001;

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern uint SetThreadExecutionState(uint esFlags);
}
