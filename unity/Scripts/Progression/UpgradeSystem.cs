using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private UpgradeCatalog upgradeCatalog;
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Health playerHealth;

    public event Action<List<UpgradeDefinition>> OptionsGenerated;
    public event Action<UpgradeDefinition> UpgradeApplied;

    public List<UpgradeDefinition> GenerateOptions(int count)
    {
        var options = new List<UpgradeDefinition>();
        var safetyCounter = 0;

        while (options.Count < count && safetyCounter < 50)
        {
            safetyCounter++;
            var rarity = RollRarity();
            var pool = upgradeCatalog.GetByRarity(rarity);
            if (pool.Count == 0)
            {
                continue;
            }

            var candidate = pool[UnityEngine.Random.Range(0, pool.Count)];
            if (!options.Contains(candidate) && IsEligible(candidate))
            {
                options.Add(candidate);
            }
        }

        OptionsGenerated?.Invoke(options);
        return options;
    }

    public bool ApplyUpgrade(UpgradeDefinition upgrade)
    {
        if (upgrade == null)
        {
            return false;
        }

        var applied = upgrade.kind switch
        {
            UpgradeKind.Stat => ApplyStatUpgrade(upgrade),
            UpgradeKind.Weapon => weaponSystem.TryAddOrUpgradeWeapon(upgrade.weapon),
            UpgradeKind.Heal => ApplyHeal(upgrade),
            UpgradeKind.Evolution => ApplyEvolution(upgrade),
            _ => false
        };

        if (applied)
        {
            UpgradeApplied?.Invoke(upgrade);
        }

        return applied;
    }

    private bool ApplyStatUpgrade(UpgradeDefinition upgrade)
    {
        if (playerStats == null)
        {
            return false;
        }

        playerStats.AddMaxHealth(upgrade.flatHealth);
        playerStats.AddDamageMultiplier(upgrade.damageMultiplier);
        playerStats.AddAttackSpeed(upgrade.attackSpeedMultiplier);
        playerStats.AddMoveSpeed(upgrade.moveSpeedMultiplier);
        playerStats.AddArea(upgrade.areaMultiplier);
        playerStats.AddArmor(upgrade.armor);
        playerStats.AddPickupRadius(upgrade.pickupRadius);
        playerStats.AddProjectileCount(upgrade.projectileCount);
        playerStats.AddLuck(upgrade.luck);
        return true;
    }

    private bool ApplyHeal(UpgradeDefinition upgrade)
    {
        if (playerHealth == null)
        {
            return false;
        }

        var healing = Mathf.Max(10, upgrade.flatHealth);
        playerHealth.Heal(healing);
        return true;
    }

    private bool ApplyEvolution(UpgradeDefinition upgrade)
    {
        if (upgrade.weapon == null)
        {
            return false;
        }

        return weaponSystem.TryEvolveWeapon(upgrade.weapon);
    }

    private bool IsEligible(UpgradeDefinition upgrade)
    {
        if (upgrade == null)
        {
            return false;
        }

        if (upgrade.kind == UpgradeKind.Weapon && upgrade.weapon == null)
        {
            return false;
        }

        return true;
    }

    private UpgradeRarity RollRarity()
    {
        var luck = playerStats != null ? playerStats.Luck : 0f;
        var roll = UnityEngine.Random.value + (luck * 0.03f);
        if (roll >= 0.92f)
        {
            return UpgradeRarity.Epic;
        }

        return roll >= 0.65f ? UpgradeRarity.Rare : UpgradeRarity.Common;
    }
}
