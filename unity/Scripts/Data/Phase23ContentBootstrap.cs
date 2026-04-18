using System.Collections.Generic;
using UnityEngine;

public class Phase23ContentBootstrap : MonoBehaviour
{
    [SerializeField] private WeaponCatalog weaponCatalog;
    [SerializeField] private UpgradeCatalog upgradeCatalog;

    [ContextMenu("Seed Catalogs")]
    public void SeedCatalogs()
    {
        if (weaponCatalog == null || upgradeCatalog == null)
        {
            return;
        }

        var weapons = new List<WeaponDefinition>
        {
            CreateWeapon("arc_bolt", "Arc Bolt", WeaponAttackPattern.Nearest, 9, 0.85f, 6f),
            CreateWeapon("orbit_blades", "Orbit Blades", WeaponAttackPattern.ClosestThree, 7, 1.05f, 4.5f),
            CreateWeapon("nova_pulse", "Nova Pulse", WeaponAttackPattern.RadialBurst, 8, 1.4f, 4f),
            CreateWeapon("piercing_spear", "Piercing Spear", WeaponAttackPattern.LineStrike, 14, 1.25f, 6.5f),
            CreateWeapon("frost_shard", "Frost Shard", WeaponAttackPattern.RandomInRange, 6, 0.45f, 5.5f),
            CreateWeapon("chain_lightning", "Chain Lightning", WeaponAttackPattern.ClosestThree, 11, 0.95f, 6f),
            CreateWeapon("void_orb", "Void Orb", WeaponAttackPattern.RadialBurst, 12, 1.8f, 5f),
            CreateWeapon("ember_wave", "Ember Wave", WeaponAttackPattern.LineStrike, 10, 1.1f, 6f)
        };

        SetCatalog(weaponCatalog, weapons);

        var upgrades = new List<UpgradeDefinition>
        {
            CreateUpgrade("power_i", "Power I", "+10% damage", UpgradeRarity.Common, 0.1f, 0f, 0f, 0f, 0f, 0f, 0, 0f, 0),
            CreateUpgrade("haste_i", "Haste I", "+8% attack speed", UpgradeRarity.Common, 0f, 0.08f, 0f, 0f, 0f, 0f, 0, 0f, 0),
            CreateUpgrade("vigor_i", "Vigor I", "+20 max HP", UpgradeRarity.Common, 0f, 0f, 0f, 0f, 0f, 0f, 0, 0f, 20),
            CreateUpgrade("swift_i", "Swift I", "+10% move speed", UpgradeRarity.Common, 0f, 0f, 0.1f, 0f, 0f, 0f, 0, 0f, 0),
            CreateUpgrade("magnet_i", "Magnet I", "+30% pickup radius", UpgradeRarity.Common, 0f, 0f, 0f, 0f, 0f, 0.3f, 0, 0f, 0),
            CreateUpgrade("armor_i", "Armor I", "+20 armor", UpgradeRarity.Rare, 0f, 0f, 0f, 0f, 20f, 0f, 0, 0f, 0),
            CreateUpgrade("focus_i", "Focus I", "+15% area", UpgradeRarity.Rare, 0f, 0f, 0f, 0.15f, 0f, 0f, 0, 0f, 0),
            CreateUpgrade("fortune_i", "Fortune I", "+1 luck", UpgradeRarity.Rare, 0f, 0f, 0f, 0f, 0f, 0f, 0, 1f, 0),
            CreateUpgrade("multishot_i", "Multishot I", "+1 projectile", UpgradeRarity.Epic, 0f, 0f, 0f, 0f, 0f, 0f, 1, 0f, 0),
            CreateUpgrade("power_ii", "Power II", "+20% damage", UpgradeRarity.Rare, 0.2f, 0f, 0f, 0f, 0f, 0f, 0, 0f, 0),
            CreateUpgrade("haste_ii", "Haste II", "+16% attack speed", UpgradeRarity.Rare, 0f, 0.16f, 0f, 0f, 0f, 0f, 0, 0f, 0),
            CreateUpgrade("emergency_heal", "Emergency Heal", "Recover 30 HP", UpgradeRarity.Common, 0f, 0f, 0f, 0f, 0f, 0f, 0, 0f, 30, UpgradeKind.Heal)
        };

        SetCatalog(upgradeCatalog, upgrades);
    }

    private static WeaponDefinition CreateWeapon(string id, string display, WeaponAttackPattern pattern, int damage, float interval, float range)
    {
        var weapon = ScriptableObject.CreateInstance<WeaponDefinition>();
        weapon.id = id;
        weapon.displayName = display;
        weapon.pattern = pattern;
        weapon.baseDamage = damage;
        weapon.baseInterval = interval;
        weapon.baseRange = range;
        weapon.maxLevel = 8;
        return weapon;
    }

    private static UpgradeDefinition CreateUpgrade(
        string id,
        string display,
        string description,
        UpgradeRarity rarity,
        float damage,
        float attackSpeed,
        float moveSpeed,
        float area,
        float armor,
        float pickup,
        int projectile,
        float luck,
        int health,
        UpgradeKind kind = UpgradeKind.Stat)
    {
        var upgrade = ScriptableObject.CreateInstance<UpgradeDefinition>();
        upgrade.id = id;
        upgrade.displayName = display;
        upgrade.description = description;
        upgrade.rarity = rarity;
        upgrade.kind = kind;
        upgrade.damageMultiplier = damage;
        upgrade.attackSpeedMultiplier = attackSpeed;
        upgrade.moveSpeedMultiplier = moveSpeed;
        upgrade.areaMultiplier = area;
        upgrade.armor = armor;
        upgrade.pickupRadius = pickup;
        upgrade.projectileCount = projectile;
        upgrade.luck = luck;
        upgrade.flatHealth = health;
        return upgrade;
    }

    private static void SetCatalog(WeaponCatalog catalog, List<WeaponDefinition> definitions)
    {
        catalog.SetDefinitions(definitions);
    }

    private static void SetCatalog(UpgradeCatalog catalog, List<UpgradeDefinition> definitions)
    {
        catalog.SetDefinitions(definitions);
    }
}
