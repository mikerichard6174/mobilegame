using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyDropXp : MonoBehaviour
{
    [SerializeField] private GameObject xpGemPrefab;
    [SerializeField] private int xpValue = 1;
    [SerializeField] private GameObject goldPickupPrefab;
    [SerializeField] private float goldDropChance = 0.35f;
    [SerializeField] private GameObject chestPickupPrefab;
    [SerializeField] private float chestDropChance = 0.05f;

    private Health health;
    private EnemyMetadata metadata;
    private RunSummaryManager runSummaryManager;

    private void Awake()
    {
        health = GetComponent<Health>();
        metadata = GetComponent<EnemyMetadata>();
        runSummaryManager = FindObjectOfType<RunSummaryManager>();
    }

    private void OnEnable()
    {
        if (health != null)
        {
            health.Died += HandleDeath;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.Died -= HandleDeath;
        }
    }

    private void HandleDeath(Health deadHealth)
    {
        if (xpGemPrefab != null)
        {
            var gem = Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
            var xpGem = gem.GetComponent<XPGem>();
            if (xpGem != null)
            {
                xpGem.SetValue(xpValue);
            }
        }

        if (goldPickupPrefab != null && Random.value <= goldDropChance)
        {
            Instantiate(goldPickupPrefab, transform.position, Quaternion.identity);
        }

        if (chestPickupPrefab != null && Random.value <= chestDropChance)
        {
            Instantiate(chestPickupPrefab, transform.position, Quaternion.identity);
        }

        var tier = metadata != null ? metadata.Tier : EnemyTier.Normal;
        runSummaryManager?.RegisterKill(tier);
    }
}
