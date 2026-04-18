using UnityEngine;

public enum UpgradeRarity
{
    Common,
    Rare,
    Epic
}

public enum UpgradeKind
{
    Stat,
    Weapon,
    Heal,
    Evolution
}

[CreateAssetMenu(menuName = "EclipseSwarm/Upgrade Definition")]
public class UpgradeDefinition : ScriptableObject
{
    public string id;
    public string displayName;
    [TextArea] public string description;
    public UpgradeRarity rarity = UpgradeRarity.Common;
    public UpgradeKind kind = UpgradeKind.Stat;

    [Header("Stat values")]
    public int flatHealth;
    public float damageMultiplier;
    public float attackSpeedMultiplier;
    public float moveSpeedMultiplier;
    public float areaMultiplier;
    public float armor;
    public float pickupRadius;
    public int projectileCount;
    public float luck;

    [Header("Weapon values")]
    public WeaponDefinition weapon;
}
