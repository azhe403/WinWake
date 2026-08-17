# WinWake — Roadmap

> Authoritative roadmap. Each phase builds on the previous. No phase is skipped.

## Overview

WinWake is built incrementally across 10 phases. Each phase produces a working, testable increment. Earlier phases prove core platform integration; later phases add the rule engine, UI, and production features.

## Phases

### Phase 1 — Basic Windows Awake Vertical Slice

Prove that WinWake can prevent Windows from entering system sleep while allowing display to turn off and Windows to lock.

- WPF application with single toggle: Keep Awake ON/OFF
- P/Invoke `SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED)`
- Timer-based re-assertion of execution state
- No process monitoring. No rule engine. No tray. No persistence.

### Phase 2 — Process Monitoring

Add ability to detect which processes are running.

- Process enumeration and monitoring
- Process start/stop event detection
- WinWake.Core interfaces for process queries
- WinWake.Windows implementation

### Phase 3 — Rule / Domain Model

Establish the rule data model and basic rule lifecycle.

- Rule model: Conditions + Policy + Priority
- Condition model: process-based conditions
- Rule CRUD operations
- Enable/disable rules

### Phase 4 — Complex Condition Tree and Rule Evaluation

Build the full condition evaluation engine.

- AND, OR, NOT boolean operators
- Nested condition groups
- Time range conditions
- Day-of-week conditions
- AC/Battery power state conditions
- Battery level conditions

### Phase 5 — Effective Policy / Policy Aggregation

Calculate effective policies from multiple active rules.

- Policy aggregation per dimension (Prevent > Allow)
- Priority-based conflict resolution
- Real-time re-evaluation when rules change
- Integration with power manager

### Phase 6 — WPF / WPF UI Rule Editor

Build the full UI for managing rules.

- Dashboard showing current effective policy
- Rule editor with condition builder
- Rule list with enable/disable
- Activity log view
- Settings view
- WPF UI theming

### Phase 7 — Configuration Persistence

Save and restore application state.

- Rule persistence (JSON or similar)
- Settings persistence
- Application state restoration on startup
- Import/export rules

### Phase 8 — System Tray and Windows Startup

Integrate with Windows desktop lifecycle.

- System tray icon with context menu
- Minimize to tray
- Start with Windows (startup registry)
- Tray notifications for policy changes

### Phase 9 — Activity / Event Logging

Record what WinWake does.

- Log rule evaluations
- Log policy changes
- Log power state transitions
- Activity view in UI
- Log rotation/cleanup

### Phase 10 — Polish, Packaging, Installer, Production Hardening

Ship it.

- MSI or MSIX installer
- Code signing
- Auto-update mechanism
- Error reporting
- Performance optimization
- Accessibility audit
- Final UI polish

## Dependencies Between Phases

```
Phase 1  (vertical slice)
  ↓
Phase 2  (process monitoring)
  ↓
Phase 3  (rule model)
  ↓
Phase 4  (condition tree)
  ↓
Phase 5  (policy aggregation)
  ↓
Phase 6  (UI rule editor)
  ↓
Phase 7  (persistence)
  ↓
Phase 8  (tray/startup)
  ↓
Phase 9  (logging)
  ↓
Phase 10 (polish/packaging)
```

Each phase assumes all previous phases are complete.
