# WinWake — Phase 1: Basic Windows Awake Vertical Slice

> Current phase. This is the authoritative definition of Phase 1 scope.

## Goal

Prove that WinWake can prevent Windows from entering system sleep while still allowing:
- Display to turn off
- Windows to lock

## Deliverables

### 1. WinWake.App (WPF)

- Minimal WPF application shell
- Single view with toggle: **Keep Awake: ON/OFF**
- ViewModel with property binding to toggle state
- DI setup with Microsoft.Extensions.DI

### 2. WinWake.Windows (P/Invoke)

- `SetThreadExecutionState()` P/Invoke declaration
- `PowerManager` service implementing `IPowerManager`
- Sleep prevention: `ES_CONTINUOUS | ES_SYSTEM_REQUIRED`
- **Do NOT use `ES_DISPLAY_REQUIRED`** — display must be allowed to turn off
- Timer-based re-assertion of execution state (Windows periodically resets it)
- Cleanup on application exit (call `SetThreadExecutionState(0)` to restore normal behavior)

### 3. WinWake.Core (Interfaces)

- `IPowerManager` interface (prevent sleep, restore sleep)
- No implementation — Core has no Windows dependency

## API Design

### IPowerManager (Core)

```csharp
namespace WinWake.Core.Services;

public interface IPowerManager
{
    void PreventSleep();
    void RestoreSleep();
}
```

### PowerManager (Windows)

```csharp
namespace WinWake.Windows.Services;

public class PowerManager : IPowerManager
{
    // Uses SetThreadExecutionState via P/Invoke
    // Manages re-assertion timer
    // Cleans up on dispose/exit
}
```

### NativeMethods (Windows)

```csharp
namespace WinWake.Windows.Native;

internal static class NativeMethods
{
    // [DllImport("kernel32.dll")]
    // internal static extern uint SetThreadExecutionState(uint esFlags);
    
    internal const uint ES_CONTINUOUS = 0x80000000;
    internal const uint ES_SYSTEM_REQUIRED = 0x00000001;
    // ES_DISPLAY_REQUIRED = 0x00000002 — NOT USED in Phase 1
}
```

## What Phase 1 Does NOT Include

- Process monitoring (Phase 2)
- Rule engine (Phase 3-4)
- Policy aggregation (Phase 5)
- Full UI with dashboard/rules/activity/settings (Phase 6)
- Configuration persistence (Phase 7)
- System tray (Phase 8)
- Activity logging (Phase 9)
- Installer/packaging (Phase 10)
- PreventLock (not implemented until rule engine exists)

## Toggle Behavior

### Keep Awake: ON

```
PreventSleep = true
SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED)
Start re-assertion timer
```

### Keep Awake: OFF

```
Stop re-assertion timer
SetThreadExecutionState(0)  // restore normal behavior
PreventSleep = false
```

## Timer Re-Assertion

Windows periodically resets the thread execution state. WinWake must periodically re-assert to maintain sleep prevention.

- Timer interval: configurable, default ~30 seconds
- Timer calls `SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED)`
- Timer stops when Keep Awake is turned OFF

## Success Criteria

1. When Keep Awake is ON, the system does not enter sleep
2. When Keep Awake is ON, the display CAN still turn off
3. When Keep Awake is OFF, normal Windows power behavior is restored
4. Re-assertion timer keeps sleep prevention active
5. Application exits cleanly and restores normal power behavior

## Projects to Create

| Project | Type | Framework |
|---------|------|-----------|
| WinWake.sln | Solution | — |
| WinWake.App | WPF App | net9.0-windows |
| WinWake.Core | Class Library | net9.0 |
| WinWake.Windows | Class Library | net9.0-windows |

## Status

```
CURRENT PHASE:  1
CURRENT TASK:   Basic Windows Awake vertical slice
CURRENT STATUS: NOT STARTED
```
