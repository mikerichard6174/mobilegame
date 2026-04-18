using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Serializable]
    public class WeaponRuntime
    {
        public WeaponDefinition definition;
        public int level = 1;
        public float nextFireTime;

        public float CurrentRange(PlayerStats stats)
        {
            return definition.baseRange * (1f + stats.AreaMultiplier);
        }

        public float CurrentInterval(PlayerStats stats)
        {
            var hasteAdjusted = definition.baseInterval / Mathf.Max(0.15f, 1f + stats.AttackSpeedMultiplier);
            var perLevelAdjustment = 1f - ((level - 1) * 0.05f);
            return Mathf.Max(0.12f, hasteAdjusted * Mathf.Max(0.55f, perLevelAdjustment));
        }

        public int CurrentDamage(PlayerStats stats)
        {
            var raw = definition.baseDamage + ((level - 1) * 3);
            return Mathf.RoundToInt(raw * (1f + stats.DamageMultiplier));
        }
    }

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private WeaponCatalog catalog;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<WeaponRuntime> equippedWeapons;

    public IReadOnlyList<WeaponRuntime> EquippedWeapons => equippedWeapons;

    public event Action<WeaponDefinition, int> WeaponLeveled;
    public event Action<WeaponDefinition> WeaponEvolved;

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState != RunState.Running)
        {
            return;
        }

        foreach (var weapon in equippedWeapons)
        {
            if (weapon.definition == null || Time.time < weapon.nextFireTime)
            {
                continue;
            }

            FireWeapon(weapon);
            weapon.nextFireTime = Time.time + weapon.CurrentInterval(playerStats);
        }
    }

    public bool TryAddOrUpgradeWeapon(WeaponDefinition definition)
    {
        if (definition == null)
        {
            return false;
        }

        var existing = equippedWeapons.Find(w => w.definition == definition);
        if (existing != null)
        {
            if (existing.level >= definition.maxLevel)
            {
                return false;
            }

            existing.level++;
            WeaponLeveled?.Invoke(definition, existing.level);
            return true;
        }

        equippedWeapons.Add(new WeaponRuntime { definition = definition, level = 1 });
        WeaponLeveled?.Invoke(definition, 1);
        return true;
    }

    public bool TryEvolveWeapon(WeaponDefinition definition)
    {
        var existing = equippedWeapons.Find(w => w.definition == definition);
        if (existing == null || definition == null || definition.evolvedWeapon == null)
        {
            return false;
        }

        if (existing.level < definition.maxLevel)
        {
            return false;
        }

        existing.definition = definition.evolvedWeapon;
        existing.level = 1;
        WeaponEvolved?.Invoke(existing.definition);
        return true;
    }

    public WeaponDefinition GetRandomStarterWeapon()
    {
        if (catalog == null || catalog.Weapons.Count == 0)
        {
            return null;
        }

        return catalog.Weapons[UnityEngine.Random.Range(0, catalog.Weapons.Count)];
    }

    private void FireWeapon(WeaponRuntime runtime)
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, runtime.CurrentRange(playerStats), enemyMask);
        if (hits.Length == 0)
        {
            return;
        }

        var damage = runtime.CurrentDamage(playerStats);
        var projectileCount = Mathf.Max(1, playerStats.ProjectileCount);

        switch (runtime.definition.pattern)
        {
            case WeaponAttackPattern.Nearest:
                DamageNearest(hits, damage, projectileCount);
                break;
            case WeaponAttackPattern.RadialBurst:
                DamageFirstN(hits, damage, Mathf.Min(hits.Length, projectileCount * 2));
                break;
            case WeaponAttackPattern.ClosestThree:
                DamageNearest(hits, damage, Mathf.Min(3 * projectileCount, hits.Length));
                break;
            case WeaponAttackPattern.RandomInRange:
                DamageRandom(hits, damage, Mathf.Min(projectileCount, hits.Length));
                break;
            case WeaponAttackPattern.LineStrike:
                DamageNearest(hits, damage * 2, Mathf.Min(projectileCount, hits.Length));
                break;
        }
    }

    private static void DamageRandom(Collider2D[] hits, int damage, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var hit = hits[UnityEngine.Random.Range(0, hits.Length)];
            TryDealDamage(hit, damage);
        }
    }

    private void DamageNearest(Collider2D[] hits, int damage, int count)
    {
        Array.Sort(hits, (a, b) =>
        {
            var da = ((Vector2)(a.transform.position - transform.position)).sqrMagnitude;
            var db = ((Vector2)(b.transform.position - transform.position)).sqrMagnitude;
            return da.CompareTo(db);
        });

        DamageFirstN(hits, damage, count);
    }

    private static void DamageFirstN(Collider2D[] hits, int damage, int count)
    {
        for (var i = 0; i < count; i++)
        {
            TryDealDamage(hits[i], damage);
        }
    }

    private static void TryDealDamage(Component hit, int damage)
    {
        var health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
