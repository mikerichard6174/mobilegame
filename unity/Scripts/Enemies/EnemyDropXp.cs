using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyDropXp : MonoBehaviour
{
    [SerializeField] private GameObject xpGemPrefab;
    [SerializeField] private int xpValue = 1;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
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
        if (xpGemPrefab == null)
        {
            return;
        }

        var gem = Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
        var xpGem = gem.GetComponent<XPGem>();
        if (xpGem != null)
        {
            xpGem.SetValue(xpValue);
        }
    }
}
