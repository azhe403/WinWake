namespace WinWake.Core.Services;

public interface IPowerManager
{
    void PreventSleep();
    void RestoreSleep();
}
