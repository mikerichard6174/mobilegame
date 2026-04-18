using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    [SerializeField] private float attackInterval = 0.6f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private int damage = 10;

    private float nextAttackTime;

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState != RunState.Running)
        {
            return;
        }

        if (Time.time < nextAttackTime)
        {
            return;
        }

        var target = FindNearestEnemy();
        if (target == null)
        {
            return;
        }

        var health = target.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        nextAttackTime = Time.time + attackInterval;
    }

    private Collider2D FindNearestEnemy()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        Collider2D nearest = null;
        var nearestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            var sqrDistance = ((Vector2)(hit.transform.position - transform.position)).sqrMagnitude;
            if (sqrDistance >= nearestDistance)
            {
                continue;
            }

            nearestDistance = sqrDistance;
            nearest = hit;
        }

        return nearest;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}
