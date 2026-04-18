using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class XPCollector : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float basePickupRadius = 1.2f;

    private CircleCollider2D pickupCollider;

    private void Awake()
    {
        pickupCollider = GetComponent<CircleCollider2D>();
        pickupCollider.isTrigger = true;
    }

    private void OnEnable()
    {
        if (playerStats != null)
        {
            playerStats.StatsChanged += RefreshRadius;
        }

        RefreshRadius();
    }

    private void OnDisable()
    {
        if (playerStats != null)
        {
            playerStats.StatsChanged -= RefreshRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var gem = other.GetComponent<XPGem>();
        if (gem != null)
        {
            xpSystem?.AddXP(gem.Value);
            Destroy(other.gameObject);
            return;
        }

        var gold = other.GetComponent<GoldPickup>();
        if (gold != null)
        {
            return;
        }
    }

    private void RefreshRadius()
    {
        var radiusMultiplier = playerStats != null ? 1f + playerStats.PickupRadiusMultiplier : 1f;
        pickupCollider.radius = basePickupRadius * radiusMultiplier;
    }
}
