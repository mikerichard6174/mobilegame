using UnityEngine;
using UnityEngine.UI;

public class RunSummaryPanel : MonoBehaviour
{
    [SerializeField] private RunSummaryManager runSummaryManager;
    [SerializeField] private GameObject panelRoot;
    [SerializeField] private Text summaryText;

    private void Awake()
    {
        if (panelRoot != null)
        {
            panelRoot.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (runSummaryManager != null)
        {
            runSummaryManager.RunEnded += HandleRunEnded;
        }
    }

    private void OnDisable()
    {
        if (runSummaryManager != null)
        {
            runSummaryManager.RunEnded -= HandleRunEnded;
        }
    }

    private void HandleRunEnded(RunSummaryData data)
    {
        if (panelRoot != null)
        {
            panelRoot.SetActive(true);
        }

        if (summaryText != null)
        {
            summaryText.text =
                $"Run Over\n" +
                $"Time: {data.SurvivalTimeSeconds:F0}s\n" +
                $"Kills: {data.Kills}\n" +
                $"Level: {data.LevelReached}\n" +
                $"Gold: {data.GoldCollected}\n" +
                $"Boss Defeated: {(data.DefeatedBoss ? "Yes" : "No")}";
        }
    }
}
