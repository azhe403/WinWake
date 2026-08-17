# WinWake — Project State

> Single authoritative source for current project state. Update this file at the start and end of every session.

```
PROJECT:       WinWake
CURRENT PHASE: 1
CURRENT TASK:  Basic Windows Awake vertical slice
CURRENT STATUS: NOT STARTED
LAST UPDATED:  2026-08-17
```

## Key Documents

| Document | Path |
|----------|------|
| Product Specification | `docs/superpowers/specs/2026-08-17-winwake-specification.md` |
| Architecture | `docs/superpowers/specs/2026-08-17-winwake-architecture.md` |
| Roadmap | `docs/superpowers/plans/2026-08-17-winwake-roadmap.md` |
| Phase 1 Plan | `docs/superpowers/plans/2026-08-17-winwake-phase-1.md` |
| This File | `docs/superpowers/specs/2026-08-17-winwake-project-state.md` |
| Desk Journal | `desks/winwake/journal.md` |

## Current Constraints

- Phase 1 scope: Keep Awake toggle only (ON/OFF)
- No rule engine yet
- No process monitoring yet
- No system tray yet
- No persistence yet
- Core must never depend on WPF or Windows APIs

## Next Actions

1. Create solution and project structure
2. Implement WinWake.Core with IPowerManager interface
3. Implement WinWake.Windows with P/Invoke and PowerManager
4. Implement WinWake.App with WPF toggle UI
5. Wire up DI
6. Test sleep prevention
