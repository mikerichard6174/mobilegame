using UnityEngine;
using UnityEngine.UI;

public class HudPresenter : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private RunSummaryManager runSummaryManager;
    [SerializeField] private Text hpText;
    [SerializeField] private Text killText;
    [SerializeField] private Text goldText;
    [SerializeField] private Text timerText;

    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged += HandleHealthChanged;
            HandleHealthChanged(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }

        if (runSummaryManager != null)
        {
            runSummaryManager.KillCountChanged += HandleKillsChanged;
            runSummaryManager.GoldChanged += HandleGoldChanged;
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= HandleHealthChanged;
        }

        if (runSummaryManager != null)
        {
            runSummaryManager.KillCountChanged -= HandleKillsChanged;
            runSummaryManager.GoldChanged -= HandleGoldChanged;
        }
    }

    private void Update()
    {
        if (timerText != null && runSummaryManager != null)
        {
            timerText.text = $"Time {runSummaryManager.CurrentRun.SurvivalTimeSeconds:F0}s";
        }
    }

    private void HandleHealthChanged(int current, int max)
    {
        if (hpText != null)
        {
            hpText.text = $"HP {Mathf.Max(0, current)}/{max}";
        }
    }

    private void HandleKillsChanged(int kills)
    {
        if (killText != null)
        {
            killText.text = $"Kills {kills}";
        }
    }

    private void HandleGoldChanged(int gold)
    {
        if (goldText != null)
        {
            goldText.text = $"Gold {gold}";
        }
    }
}
