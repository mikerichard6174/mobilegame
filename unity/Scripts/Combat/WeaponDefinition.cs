using UnityEngine;

public enum WeaponAttackPattern
{
    Nearest,
    RadialBurst,
    ClosestThree,
    RandomInRange,
    LineStrike
}

[CreateAssetMenu(menuName = "EclipseSwarm/Weapon Definition")]
public class WeaponDefinition : ScriptableObject
{
    [Header("Identity")]
    public string id;
    public string displayName;

    [Header("Tuning")]
    public WeaponAttackPattern pattern = WeaponAttackPattern.Nearest;
    public int maxLevel = 8;
    public int baseDamage = 8;
    public float baseInterval = 1f;
    public float baseRange = 5.5f;

    [Header("Evolution")]
    public string requiredPassiveId;
    public WeaponDefinition evolvedWeapon;
}
