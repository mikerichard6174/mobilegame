using System.Collections.Generic;
using UnityEngine;

public class UpgradeCatalog : MonoBehaviour
{
    [SerializeField] private List<UpgradeDefinition> upgradeDefinitions;

    public IReadOnlyList<UpgradeDefinition> Upgrades => upgradeDefinitions;

    public void SetDefinitions(List<UpgradeDefinition> definitions)
    {
        upgradeDefinitions = definitions;
    }

    public List<UpgradeDefinition> GetByRarity(UpgradeRarity rarity)
    {
        return upgradeDefinitions.FindAll(u => u != null && u.rarity == rarity);
    }
}
