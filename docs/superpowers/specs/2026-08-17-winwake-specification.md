# WinWake — Product Specification

> Authoritative product specification. All implementation must conform to this document.

## Overview

WinWake is a Windows desktop utility for controlling Windows power behavior through configurable rules.

The user defines rules that specify what power behaviors to prevent or allow based on conditions. WinWake evaluates all active rules and calculates an effective policy for each power dimension.

## Policy Dimensions

WinWake independently controls three policy dimensions:

| Dimension | Description |
|-----------|-------------|
| **Sleep** | System sleep / suspend |
| **Lock** | Windows lock screen |
| **Display** | Display sleep / off |

Each dimension has two possible states in a rule:

- **Prevent** — actively prevent this behavior
- **Allow** — permit normal Windows behavior

## Policy Merging

Multiple rules may be active simultaneously. The system calculates an **Effective Policy** from all active rules.

For each policy dimension:

```
Prevent > Allow
```

If any active rule says Prevent for a dimension, the effective policy for that dimension is Prevent.

### Example

```
OpenCode is running:
    Sleep   = Prevent
    Lock    = Allow
    Display = Allow

Blender is rendering:
    Sleep   = Prevent
    Lock    = Prevent
    Display = Prevent

Effective Policy (both rules active):
    Sleep   = Prevent  (both say Prevent)
    Lock    = Prevent  (Blender wins: Prevent > Allow)
    Display = Prevent  (Blender wins: Prevent > Allow)
```

## Rule Structure

A rule consists of:

```
Rule = Conditions + Policy + Priority
```

### Conditions

Conditions determine when a rule is active. Eventually conditions support:

- Process conditions (is a specific process running)
- Boolean operators: AND, OR, NOT
- Nested condition groups
- Time ranges (e.g., 9:00 AM to 5:00 PM)
- Day of week (e.g., Monday through Friday)
- AC/Battery power state
- Battery level thresholds
- Other useful Windows/session conditions

### Policy

The policy specifies what to do for each dimension when the rule is active:

```
Policy = {
    Sleep:   Prevent | Allow,
    Lock:    Prevent | Allow,
    Display: Prevent | Allow
}
```

### Priority

Priority determines precedence when rules conflict (same dimension, different directives).

## UI

The eventual UI contains four sections:

- **Dashboard** — overview of current state
- **Rules** — create, edit, enable/disable rules
- **Activity** — log of rule evaluations and state changes
- **Settings** — application configuration

UI technology: WPF + WPF UI library.

**Hard constraint:** The UI must not contain rule-engine or Windows power-management logic. All logic lives in Core or Windows projects.

## Tech Stack

| Component | Technology |
|-----------|-----------|
| Framework | .NET 9 |
| Language | C# 13 |
| UI | WPF + WPF UI |
| MVVM | CommunityToolkit.Mvvm |
| DI | Microsoft.Extensions.DI |
| Architecture | 3-project (App, Core, Windows) |

## Constraints

1. WinWake.Core must never depend on WPF, Windows APIs, or platform-specific code.
2. WinWake.App contains only UI and DI composition.
3. WinWake.Windows contains all Windows API integration.
4. The rule engine is UI-independent (lives in Core).
5. Prevent always takes precedence over Allow in policy merging.
