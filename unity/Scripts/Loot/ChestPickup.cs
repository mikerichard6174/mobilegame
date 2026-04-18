using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ChestPickup : MonoBehaviour
{
    [SerializeField] private ChestRewardSystem chestRewardSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || chestRewardSystem == null)
        {
            return;
        }

        chestRewardSystem.OpenChest();
        Destroy(gameObject);
    }
}
