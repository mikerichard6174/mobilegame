using UnityEngine;

public class FeedbackEventRouter : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private ChestRewardSystem chestRewardSystem;
    [SerializeField] private Health playerHealth;
    [SerializeField] private AudioFeedbackSystem audioFeedback;
    [SerializeField] private HapticFeedbackSystem hapticFeedback;

    private void OnEnable()
    {
        if (xpSystem != null)
        {
            xpSystem.LevelUpTriggered += HandleLevelUp;
        }

        if (chestRewardSystem != null)
        {
            chestRewardSystem.ChestRewardGranted += HandleChestOpened;
        }

        if (playerHealth != null)
        {
            playerHealth.HealthChanged += HandleHealthChanged;
        }
    }

    private void OnDisable()
    {
        if (xpSystem != null)
        {
            xpSystem.LevelUpTriggered -= HandleLevelUp;
        }

        if (chestRewardSystem != null)
        {
            chestRewardSystem.ChestRewardGranted -= HandleChestOpened;
        }

        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= HandleHealthChanged;
        }
    }

    private void HandleLevelUp(int level)
    {
        audioFeedback?.PlayLevelUp();
        hapticFeedback?.TriggerLightImpact();
    }

    private void HandleChestOpened(string reward)
    {
        audioFeedback?.PlayChestOpen();
        hapticFeedback?.TriggerHeavyImpact();
    }

    private void HandleHealthChanged(int current, int max)
    {
        if (current <= Mathf.CeilToInt(max * 0.2f))
        {
            audioFeedback?.PlayNearDeath();
        }
    }
}
