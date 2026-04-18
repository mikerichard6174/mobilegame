using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerDeathHandler : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        if (health != null)
        {
            health.Died += HandlePlayerDeath;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.Died -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath(Health deadHealth)
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.SetGameOver();
        }
    }
}
