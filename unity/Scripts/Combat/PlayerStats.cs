using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int baseMaxHealth = 100;
    [SerializeField] private float baseMoveSpeed = 5f;

    public int MaxHealthBonus { get; private set; }
    public float DamageMultiplier { get; private set; }
    public float AttackSpeedMultiplier { get; private set; }
    public float MoveSpeedMultiplier { get; private set; }
    public float AreaMultiplier { get; private set; }
    public float Armor { get; private set; }
    public float PickupRadiusMultiplier { get; private set; }
    public int ProjectileCount { get; private set; } = 1;
    public float Luck { get; private set; }

    public event Action StatsChanged;

    public int CurrentMaxHealth => Mathf.RoundToInt((baseMaxHealth + MaxHealthBonus));
    public float CurrentMoveSpeed => baseMoveSpeed * (1f + MoveSpeedMultiplier);

    public void AddMaxHealth(int amount)
    {
        MaxHealthBonus += amount;
        StatsChanged?.Invoke();
    }

    public void AddDamageMultiplier(float amount)
    {
        DamageMultiplier += amount;
        StatsChanged?.Invoke();
    }

    public void AddAttackSpeed(float amount)
    {
        AttackSpeedMultiplier += amount;
        StatsChanged?.Invoke();
    }

    public void AddMoveSpeed(float amount)
    {
        MoveSpeedMultiplier += amount;
        StatsChanged?.Invoke();
    }

    public void AddArea(float amount)
    {
        AreaMultiplier += amount;
        StatsChanged?.Invoke();
    }

    public void AddArmor(float amount)
    {
        Armor += amount;
        StatsChanged?.Invoke();
    }

    public void AddPickupRadius(float amount)
    {
        PickupRadiusMultiplier += amount;
        StatsChanged?.Invoke();
    }

    public void AddProjectileCount(int amount)
    {
        ProjectileCount = Mathf.Max(1, ProjectileCount + amount);
        StatsChanged?.Invoke();
    }

    public void AddLuck(float amount)
    {
        Luck += amount;
        StatsChanged?.Invoke();
    }
}
