# Game Design Document (GDD-lite)

## 1) Vision

**Working title:** Eclipse Swarm  
**Genre:** Survivors-like action roguelite  
**Platform:** iOS + Android (portrait optional, landscape recommended for readability)  
**Session length target:** 10–20 minutes/run

### Design pillars

1. **Immediate power fantasy** – player feels strong in under 30 seconds.
2. **Readable chaos** – many enemies, but always understandable threat zones.
3. **Meaningful builds** – upgrades create distinct playstyles every run.
4. **Mobile comfort** – one-thumb movement + low cognitive UI.

## 2) Core Loop

1. Spawn into map with one starter weapon.
2. Move to kite enemies while weapons auto-fire.
3. Collect XP gems to level up.
4. Choose from 3 random upgrades each level.
5. Survive timed waves with elite/boss spikes.
6. End run by death or timer completion.
7. Convert run rewards into persistent upgrades/unlocks.

## 3) MVP Scope (Vertical Slice)

### Playable content

- 1 hero.
- 1 map (single biome).
- 6 normal enemy archetypes + 1 elite + 1 boss.
- 8 weapons.
- 12 passive upgrades.
- 1 full 12-minute run mode.

### Success criteria for MVP

- Stable 60 FPS target on mid-tier device under heavy spawn load.
- Average test-run length > 6 minutes for new players.
- At least 3 distinct viable builds.

## 4) Controls and Input

- **Primary control:** virtual joystick (left side).
- **No manual fire** in base mode (auto attack).
- Optional skill button later for hero-specific active.
- Touch targets minimum 48dp.

## 5) Combat Systems

## Player stats

- Max HP
- Move Speed
- Armor / Damage Reduction
- Damage
- Attack Speed
- Projectile Count
- Pickup Radius
- Luck (affects rarity/offer quality)

## Weapon behaviors (sample)

1. **Arc Bolt** – nearest-target projectile.
2. **Orbit Blades** – rotating defense ring.
3. **Nova Pulse** – periodic radial burst.
4. **Piercing Spear** – line attack through enemies.

## Upgrade model

- Offer 3 choices on level-up.
- Rarity tiers: Common / Rare / Epic.
- Cap individual weapon level at 8.
- Combine maxed weapon + passive for evolution.

## 6) Enemy and Wave Design

## Enemy archetypes

- Swarm runner: low HP, high speed.
- Bruiser: high HP, low speed.
- Ranged spitter: projectile pressure.
- Splitter: divides on death.
- Charger: telegraphed dash.
- Aura unit: buffs nearby enemies.

## Wave pacing

- Minute 0–2: onboarding and early reward frequency.
- Minute 3–7: density ramps + mixed archetypes.
- Minute 8–10: elites + hazard layering.
- Final phase: boss plus sustained pressure.

## 7) Progression and Meta

## In-run progression

- XP levels for temporary upgrades.
- Gold drops for run-end currency bonus.
- Chests for higher rarity spikes.

## Meta progression

- Permanent tree with modest linear gains (avoid pay-to-win feel).
- Hero unlocks through challenges.
- Weapon unlocks tied to mastery achievements.

## 8) Economy and Monetization (player-respectful)

- Cosmetic skins.
- Optional ad revive (once per run).
- Battle pass only after core retention metrics are healthy.
- No hard content walls tied to spending.

## 9) UX and UI Requirements

- Minimal HUD: HP, level bar, timer, pause, mini kill counter.
- Level-up screen pauses gameplay fully.
- Strong readability for damage numbers and pickup items.
- Haptics for level-up, chest open, and near-death events.

## 10) Audio Direction

- Layered music intensity by danger level.
- Distinct SFX families for weapon categories.
- Audible cues for elite spawn and boss attacks.

## 11) Technical Constraints

- Object pooling mandatory for projectiles/enemies/pickups.
- Fixed update budget to avoid frame spikes.
- Sprite atlas batching and limited overdraw.
- Deterministic-ish spawn scheduling for easier balancing.

## 12) First Playtest Checklist

- Can players understand movement immediately?
- Are level-up choices exciting and clear?
- Are deaths perceived as fair?
- Is visual noise manageable at peak density?
- Does the run feel too long/too short?

## 13) Current implementation status

Phase 1 foundation is now implemented in code:

- Player movement (virtual joystick + keyboard fallback).
- Enemy spawning and chase behavior.
- Starter auto-weapon targeting and damage loop.
- XP gem drops, XP collection, level thresholds, and level-up pause panel.
