# Technical Architecture and Build Roadmap (Unity Mobile)

## Engine and stack choice

**Recommended:** Unity 6 (2D URP), C#, Addressables, Unity Input System.

Why this stack:

- Mature mobile export pipeline.
- Strong profiling tooling.
- Easy content iteration with ScriptableObjects.
- Large talent/tutorial ecosystem for survivors-like mechanics.

## High-level architecture

### Core runtime systems

- `GameStateManager` – run states (menu, running, paused, level-up, game-over).
- `WaveDirector` – time-based spawn budget and enemy composition.
- `EnemySpawner` – pulls enemies from pools, applies spawn rules.
- `PlayerController` – joystick-driven movement.
- `WeaponSystem` – auto-fire timing + target acquisition.
- `UpgradeSystem` – weighted random options and application.
- `XPSystem` – XP pickup, thresholds, level events.
- `LootSystem` – chest and currency drops.
- `MetaProgressionSystem` – persistent upgrades/unlocks.

### Data-driven assets (ScriptableObjects)

- Hero definitions
- Weapon definitions
- Passive upgrade definitions
- Enemy definitions
- Wave table definitions
- Drop tables

### Performance strategy

- Aggressive pooling for all frequently spawned entities.
- Avoid per-frame allocations (`GC.Alloc` spikes).
- Batch pathing updates and distance checks.
- Use squared distance checks where possible.
- Cap simultaneous active enemies by device tier profile.

## Suggested folder structure

```text
Assets/
  Scripts/
    Core/
    Combat/
    Enemies/
    Progression/
    Input/
    UI/
  Data/
    Heroes/
    Weapons/
    Upgrades/
    Enemies/
    Waves/
  Prefabs/
  Art/
  Audio/
```

## Build plan (12 weeks)

### Phase 1 (Weeks 1-2): Playable foundation (COMPLETED IN REPO)

Implemented scripts now cover all phase-1 goals:

- Player movement with joystick (`VirtualJoystick` + `PlayerController`).
- Enemy spawn + chase behavior (`EnemySpawner`, `EnemyChase`).
- One weapon auto-fire (`AutoWeapon`).
- XP drops + level-up popup (`EnemyDropXp`, `XPGem`, `XPCollector`, `XPSystem`, `LevelUpPanel`).
- Run-state flow with pause-on-level-up (`GameStateManager`, `BootstrapRunState`).

### Phase 2 (Weeks 3-5): Core depth

- Add 6–8 weapons.
- Add 10+ upgrades and rarity system.
- Implement elite and boss logic.
- Add chest rewards and evolution mechanic.

### Phase 3 (Weeks 6-8): Meta and UX

- Permanent progression tree.
- Unlock flows and run-end summary.
- Better HUD readability and feedback.
- Audio pass + haptics.

### Phase 4 (Weeks 9-10): Mobile optimization

- Device-tier quality settings.
- Frame-time and thermal tuning.
- Memory and load-time optimization.
- Crash and analytics instrumentation.

### Phase 5 (Weeks 11-12): Soft-launch prep

- FTUE onboarding.
- Economy tuning.
- A/B tests for upgrade offer frequency.
- Store assets and release checklist.

## Phase 1 wiring checklist (scene setup)

1. Create `GameSystems` object and attach `GameStateManager` + `BootstrapRunState`.
2. Player object:
   - Tag as `Player`.
   - Add `Rigidbody2D`, collider, `Health`, `PlayerDeathHandler`, `PlayerController`, `AutoWeapon`, `XPCollector`.
3. UI canvas:
   - Add joystick background/handle and `VirtualJoystick`.
   - Add XP slider and `XPBarPresenter`.
   - Add level-up panel and `LevelUpPanel` with 3 option buttons.
4. Enemy prefab:
   - Add collider, `Rigidbody2D`, `Health`, `EnemyChase`, `EnemyDropXp`.
5. XP gem prefab:
   - Add trigger collider + `XPGem`.
6. Spawner object:
   - Add `EnemySpawner`, assign enemy prefab list and player transform.
7. XP system object:
   - Add `XPSystem`; connect to `XPCollector`, `XPBarPresenter`, and `LevelUpPanel`.

## Telemetry (minimum viable analytics)

Track at least:

- Session start/end.
- Run duration.
- Cause of death.
- Chosen upgrades distribution.
- Retention D1/D3/D7.
- Ad revive usage.

## Technical risks and mitigations

1. **Late-game frame drops**  
   Mitigate with hard entity caps, pooling, and simplified VFX at density thresholds.
2. **Build balance collapse**  
   Mitigate via simulation tools and telemetry-driven patch cadence.
3. **UI overwhelm on small screens**  
   Mitigate with hierarchy reduction and progressive disclosure.
