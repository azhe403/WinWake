# WinWake — Architecture

> Authoritative architecture document. All code organization must conform to this document.

## Solution Structure

```
WinWake.sln
├── src/
│   ├── WinWake.App/          WPF application, UI, DI composition root
│   ├── WinWake.Core/         Domain models, rules, conditions, policy engine
│   └── WinWake.Windows/      Windows APIs, PInvoke, process monitoring
└── docs/
    └── superpowers/
        ├── specs/            Specifications
        └── plans/            Roadmap and phase plans
```

## Projects

### WinWake.App

**Role:** Presentation layer and composition root.

- WPF application entry point
- XAML views and view models
- CommunityToolkit.Mvvm (ObservableProperty, RelayCommand)
- Microsoft.Extensions.DI service registration
- Contains NO rule engine logic
- Contains NO Windows API calls for power management

**Dependencies:** WinWake.Core, WinWake.Windows

### WinWake.Core

**Role:** Domain logic, platform-independent.

- Domain models (Rule, Condition, Policy)
- Condition tree evaluation engine
- Policy aggregation (Prevent > Allow)
- Power policy state model
- Rule evaluation logic

**Dependencies:** None (application-specific). Uses only .NET base libraries.

**Hard constraint:** Must NOT depend on:
- WPF or any UI framework
- WinWake.App
- WinWake.Windows
- Any Windows-specific API

### WinWake.Windows

**Role:** Windows platform integration.

- P/Invoke declarations (SetThreadExecutionState, etc.)
- Process monitoring (which processes are running)
- Power management (prevent/restore sleep, lock, display)
- Windows session state detection
- Power state monitoring (AC/Battery)

**Dependencies:** WinWake.Core

**Notes:**
- Must handle periodic re-assertion of execution state (Windows resets it)
- Must clean up on application exit (restore normal power behavior)

## Dependency Graph

```
WinWake.App ──────> WinWake.Core <────── WinWake.Windows
```

Arrows indicate "depends on". Note the direction:

- App depends on Core and Windows
- Windows depends on Core
- Core depends on nothing application-specific

## Dependency Injection

All three projects participate in DI:

- **WinWake.App** — composition root, registers all services
- **WinWake.Core** — registers domain services
- **WinWake.Windows** — registers platform services

Service lifetimes follow standard DI conventions:
- **Singleton** for state-holding services (power manager, rule evaluator)
- **Transient** for stateless helpers
- **Scoped** where appropriate (e.g., per-evaluation-scope)

## Data Flow

```
User toggles "Keep Awake"
    → App (ViewModel) calls Core service
    → Core calculates effective policy
    → Core calls Windows service to apply policy
    → Windows calls SetThreadExecutionState()
    → Timer re-asserts periodically
```

## File Organization

Each project follows this internal structure:

```
WinWake.Core/
├── Models/
│   ├── Rule.cs
│   ├── Condition/
│   ├── Policy.cs
│   └── PolicyDimension.cs
├── Engine/
│   ├── RuleEvaluator.cs
│   ├── ConditionEvaluator.cs
│   └── PolicyAggregator.cs
└── Services/
    ├── IRuleService.cs
    └── IPolicyService.cs

WinWake.Windows/
├── Native/
│   ├── ThreadExecutionState.cs
│   └── NativeMethods.cs
├── Services/
│   ├── IPowerManager.cs
│   ├── PowerManager.cs
│   ├── IProcessMonitor.cs
│   └── ProcessMonitor.cs
└── Timers/
    └── ExecutionStateTimer.cs

WinWake.App/
├── Views/
│   ├── MainWindow.xaml
│   ├── DashboardView.xaml
│   ├── RulesView.xaml
│   ├── ActivityView.xaml
│   └── SettingsView.xaml
├── ViewModels/
│   ├── MainViewModel.cs
│   ├── DashboardViewModel.cs
│   ├── RulesViewModel.cs
│   ├── ActivityViewModel.cs
│   └── SettingsViewModel.cs
├── Services/
│   └── NavigationService.cs
├── App.xaml.cs          (DI composition root)
└── Program.cs           (entry point)
```
