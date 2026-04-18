using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GoldPickup : MonoBehaviour
{
    [SerializeField] private int goldAmount = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        var runSummary = FindObjectOfType<RunSummaryManager>();
        runSummary?.AddGold(goldAmount);
        Destroy(gameObject);
    }
}
