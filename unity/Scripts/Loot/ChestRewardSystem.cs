using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestRewardSystem : MonoBehaviour
{
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private WeaponCatalog weaponCatalog;

    public event Action<string> ChestRewardGranted;

    public void OpenChest()
    {
        var evolved = TryGrantEvolution();
        if (evolved)
        {
            return;
        }

        var options = new List<string>();
        if (TryGrantWeaponUpgrade())
        {
            options.Add("Weapon Upgrade");
        }

        var upgrades = upgradeSystem != null ? upgradeSystem.GenerateOptions(1) : new List<UpgradeDefinition>();
        if (upgradeSystem != null && upgrades.Count > 0 && upgradeSystem.ApplyUpgrade(upgrades[0]))
        {
            options.Add(upgrades[0].displayName);
        }

        if (options.Count == 0)
        {
            options.Add("Gold Bonus");
        }

        ChestRewardGranted?.Invoke(string.Join(" + ", options));
    }

    private bool TryGrantEvolution()
    {
        foreach (var runtime in weaponSystem.EquippedWeapons)
        {
            var weapon = runtime.definition;
            if (weapon == null || weapon.evolvedWeapon == null || runtime.level < weapon.maxLevel)
            {
                continue;
            }

            if (weaponSystem.TryEvolveWeapon(weapon))
            {
                ChestRewardGranted?.Invoke($"Evolved: {weapon.evolvedWeapon.displayName}");
                return true;
            }
        }

        return false;
    }

    private bool TryGrantWeaponUpgrade()
    {
        if (weaponCatalog == null)
        {
            return false;
        }

        var definition = weaponCatalog.Weapons.Count > 0
            ? weaponCatalog.Weapons[UnityEngine.Random.Range(0, weaponCatalog.Weapons.Count)]
            : null;

        return definition != null && weaponSystem.TryAddOrUpgradeWeapon(definition);
    }
}
