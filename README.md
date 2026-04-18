# Eclipse Swarm (Mobile Survivors-Like)

A starter Unity kit for building a mobile survivors-like game with a completed **Phase 1 + 2 + 3** foundation.

## Included systems

- Core run loop and game-state flow.
- Mobile joystick movement and scalable player stats.
- Multi-weapon combat architecture (8-weapon-ready) with evolution hooks.
- Upgrade draft system (3 options, weighted rarities, 12 baseline upgrades).
- Elite/boss spawn pacing.
- Chest rewards, gold economy, and run-summary tracking.
- Persistent meta progression nodes and unlock logic.
- HUD, run-end summary panel, audio cues, and haptic hooks.

## Project contents

- `docs/game-design.md` – product/experience design reference.
- `docs/technical-roadmap.md` – implementation status + scene wiring for phases 1–3.
- `unity/Scripts/` – C# runtime systems.

## Quick start

1. Open Unity 2D URP project.
2. Copy `unity/Scripts/` into `Assets/Scripts/`.
3. Follow the scene wiring checklist in `docs/technical-roadmap.md`.
4. Seed weapon/upgrade catalogs via `Phase23ContentBootstrap` context menu.
5. Press play to verify:
   - Start with a weapon and auto-combat.
   - Level-up pauses and offers 3 upgrades.
   - Elite/boss enemies appear over time.
   - Chests grant upgrades/evolution opportunities.
   - Run-over shows summary and applies meta rewards.

## Important note

Build unique content and identity (art, lore, balance, naming). Use genre conventions, not copied assets/content.
