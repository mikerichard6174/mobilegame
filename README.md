# Eclipse Swarm (Mobile Survivors-Like)

A starter design and implementation kit for building a mobile game inspired by the *survivors-like* genre (arena survival, auto-attacks, escalating enemy waves, and build-defining upgrades).

> Goal: go from idea to a playable **Phase 1 vertical slice** quickly with a mobile-first setup.

## What is included

- Product vision and design pillars.
- A complete gameplay loop and progression model.
- Mobile-specific controls, performance, and monetization considerations.
- Unity-oriented architecture notes and C# starter scripts.
- A completed **Phase 1** systems pass (movement, spawn/chase, auto-fire, XP and level-up flow).

## Contents

- `docs/game-design.md` – Core game design document (GDD-lite).
- `docs/technical-roadmap.md` – Implementation architecture + phased plan + scene wiring checklist.
- `unity/Scripts/` – Unity scripts for runnable phase-1 foundation.

## Quick start (Unity)

1. Create a fresh Unity 2D URP project.
2. Copy the `unity/Scripts/` folder into `Assets/Scripts/`.
3. Follow the **Phase 1 wiring checklist** in `docs/technical-roadmap.md`.
4. Hit play and validate the loop:
   - Move with joystick/keyboard.
   - Enemies spawn and chase.
   - Weapon auto-attacks nearest enemy.
   - Kills drop XP gems.
   - XP levels trigger a pause + level-up panel.

## Important creative/legal note

Build your own identity: unique art direction, lore, progression systems, balancing, and names. Use genre conventions, not copied assets/content.
