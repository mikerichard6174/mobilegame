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
- `EnemySpawner` – normal/elite/boss spawn pacing.
- `PlayerController` – joystick-driven movement with stat scaling.
- `WeaponSystem` – multi-weapon auto-fire patterns and evolution.
- `UpgradeSystem` – weighted random options with rarity tiers.
- `XPSystem` – XP pickup, thresholds, level events.
- `ChestRewardSystem` – chest flow and evolution reward checks.
- `MetaProgressionSystem` – persistent progression nodes and run-end banking.
- `RunSummaryManager` – end-of-run stats for UX + unlock pipeline.

### Data-driven assets (ScriptableObjects)

- Weapon definitions (8 baseline weapons)
- Upgrade definitions (12 baseline upgrades)
- Enemy prefabs separated by tier (normal/elite/boss)

## Suggested folder structure

```text
Assets/
  Scripts/
    Core/
    Combat/
    Enemies/
    Progression/
    Input/
    Loot/
    UI/
    Feedback/
    Meta/
```

## Build plan status

### Phase 1 (Weeks 1-2): Playable foundation ✅ Complete

- Player movement with joystick.
- Enemy spawn + chase behavior.
- One weapon auto-fire.
- XP drops + level-up popup.

### Phase 2 (Weeks 3-5): Core depth ✅ Complete

Implemented in code:

- 8-weapon architecture and runtime leveling (`WeaponSystem` + `WeaponCatalog` + `WeaponDefinition`).
- Rarity-based upgrades with 3 choices (`UpgradeSystem`, `UpgradeCatalog`, `UpgradeDefinition`, `LevelUpPanel`).
- Elite and boss pacing in `EnemySpawner`.
- Chest rewards and evolution path via `ChestRewardSystem` + `ChestPickup`.

### Phase 3 (Weeks 6-8): Meta and UX ✅ Complete

Implemented in code:

- Persistent progression tree with spendable run currency (`MetaProgressionSystem`).
- Unlock flow using run summary thresholds (`UnlockSystem`).
- Run-end summary panel and tracked stats (`RunSummaryManager`, `RunSummaryPanel`).
- Improved HUD presenter (`HudPresenter`) and progression visuals (`XPBarPresenter`).
- Audio + haptic feedback hooks (`AudioFeedbackSystem`, `HapticFeedbackSystem`, `FeedbackEventRouter`).

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

## Scene wiring checklist (Phase 1–3)

1. `GameSystems` object:
   - `GameStateManager`
   - `BootstrapRunState`
   - `RunSummaryManager`
   - `MetaProgressionSystem`
   - `UnlockSystem`
2. Player object (`tag: Player`):
   - `Rigidbody2D` + collider
   - `PlayerStats`
   - `Health` (`usePlayerStatsAsMaxHealth = true`)
   - `PlayerController`
   - `WeaponSystem`
   - `StarterLoadout`
   - `XPCollector` (circle trigger)
   - `PlayerDeathHandler`
3. Catalog objects:
   - `WeaponCatalog`
   - `UpgradeCatalog`
   - `Phase23ContentBootstrap` (run context menu `Seed Catalogs`)
4. Enemy setup:
   - Normal/elite/boss prefabs each include `Health`, `EnemyMetadata`, `EnemyChase`, `EnemyDropXp`
   - Spawner includes `EnemySpawner` and prefab references
5. Loot setup:
   - XP gem prefab with `XPGem`
   - Gold pickup prefab with `GoldPickup`
   - Chest prefab with `ChestPickup`
   - One object with `ChestRewardSystem`
6. UI setup:
   - `LevelUpPanel` + three `UpgradeOptionView` buttons
   - XP slider + `XPBarPresenter`
   - HUD labels + `HudPresenter`
   - Run-end panel + `RunSummaryPanel`
7. Feedback setup:
   - `AudioFeedbackSystem`
   - `HapticFeedbackSystem`
   - `FeedbackEventRouter`
