# Desk: winwake

## Purpose

Persistent project state for **WinWake** — a Windows desktop utility for controlling Windows power behavior through configurable rules.

## Project

- **Name:** WinWake
- **Platform:** Windows desktop (WPF)
- **Stack:** .NET 9, C# 13, WPF + WPF UI, CommunityToolkit.Mvvm, Microsoft.Extensions.DI
- **Working Directory:** `C:\Projexts\Space\WinWake`
- **Git:** Not yet initialized

## Current State

```
PROJECT:      WinWake
CURRENT PHASE: 1
CURRENT TASK:  Basic Windows Awake vertical slice
CURRENT STATUS: NOT STARTED
```

## Architecture Summary

Three projects with strict dependency rules:

```
WinWake.App      (WPF UI, CommunityToolkit.Mvvm)
    -> WinWake.Core
    -> WinWake.Windows

WinWake.Windows  (Windows APIs, PInvoke, process monitoring)
    -> WinWake.Core

WinWake.Core     (Domain models, rules, conditions, policy engine)
    -> nothing application-specific
```

**Hard constraint:** WinWake.Core must NEVER depend on WPF, Windows APIs, or any platform-specific implementation.

## Key Decisions

1. **Prevent > Allow** — when merging policies from active rules, Prevent always wins.
2. **Core is platform-agnostic** — rule engine lives in Core, Windows integration lives in Windows project.
3. **Timer-based re-assertion** — Windows periodically resets execution state; WinWake must re-assert via timer.
4. **CommunityToolkit.Mvvm** — for MVVM bindings, ObservableProperty, RelayCommand.
5. **Microsoft.Extensions.DI** — for dependency injection across all three projects.

## Roadmap

See `docs/superpowers/plans/2026-08-17-winwake-roadmap.md` for full roadmap.

| Phase | Focus |
|-------|-------|
| 1 | Basic Windows Awake vertical slice |
| 2 | Process monitoring |
| 3 | Rule/domain model |
| 4 | Complex condition tree and rule evaluation |
| 5 | Effective policy / policy aggregation |
| 6 | WPF/WPF UI rule editor |
| 7 | Configuration persistence |
| 8 | System tray and Windows startup |
| 9 | Activity/event logging |
| 10 | Polish, packaging, installer, production hardening |

## Session History

### 2026-08-17 — Bootstrap

- Project bootstrapped from scratch.
- Full product specification, architecture, roadmap, and Phase 1 definition established.
- No application code written. Documentation only.
- All authoritative docs created under `docs/superpowers/`.
