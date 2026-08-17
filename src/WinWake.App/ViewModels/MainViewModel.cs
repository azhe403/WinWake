using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinWake.Core.Services;

namespace WinWake.App.ViewModels;

public partial class MainViewModel : ObservableObject, IDisposable
{
    private readonly IPowerManager _powerManager;
    private bool _disposed;

    public MainViewModel(IPowerManager powerManager)
    {
        _powerManager = powerManager;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StatusText))]
    private bool _isKeepAwakeEnabled;

    public string StatusText => IsKeepAwakeEnabled ? "System sleep is PREVENTED" : "Normal sleep behavior";

    [RelayCommand]
    private void ToggleKeepAwake()
    {
        IsKeepAwakeEnabled = !IsKeepAwakeEnabled;

        if (IsKeepAwakeEnabled)
            _powerManager.PreventSleep();
        else
            _powerManager.RestoreSleep();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _powerManager.RestoreSleep();
        GC.SuppressFinalize(this);
    }
}
