using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 25;
    [SerializeField] private bool usePlayerStatsAsMaxHealth;
    [SerializeField] private PlayerStats playerStats;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public event Action<Health> Died;
    public event Action<int, int> HealthChanged;

    private void Awake()
    {
        MaxHealth = usePlayerStatsAsMaxHealth && playerStats != null ? playerStats.CurrentMaxHealth : maxHealth;
        CurrentHealth = MaxHealth;

        if (playerStats != null)
        {
            playerStats.StatsChanged += HandleStatsChanged;
        }

        HealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.StatsChanged -= HandleStatsChanged;
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        var mitigated = Mathf.RoundToInt(amount);
        if (playerStats != null)
        {
            mitigated = Mathf.Max(1, Mathf.RoundToInt(amount * (100f / (100f + playerStats.Armor))));
        }

        CurrentHealth -= mitigated;
        HealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth > 0)
        {
            return;
        }

        Died?.Invoke(this);
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        HealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void HandleStatsChanged()
    {
        if (!usePlayerStatsAsMaxHealth || playerStats == null)
        {
            return;
        }

        var previousMax = MaxHealth;
        MaxHealth = playerStats.CurrentMaxHealth;
        CurrentHealth += MaxHealth - previousMax;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        HealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
