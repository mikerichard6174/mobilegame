using System;
using UnityEngine;

[Serializable]
public class RunSummaryData
{
    public int Kills;
    public int LevelReached;
    public int GoldCollected;
    public float SurvivalTimeSeconds;
    public bool DefeatedBoss;
}

public class RunSummaryManager : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private MetaProgressionSystem metaProgression;

    public RunSummaryData CurrentRun { get; } = new RunSummaryData();

    public event Action<RunSummaryData> RunEnded;
    public event Action<int> KillCountChanged;
    public event Action<int> GoldChanged;

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState == RunState.Running)
        {
            CurrentRun.SurvivalTimeSeconds += Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.StateChanged += HandleStateChanged;
        }

        if (xpSystem != null)
        {
            xpSystem.LevelUpTriggered += HandleLevelChanged;
        }
    }

    private void OnDisable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.StateChanged -= HandleStateChanged;
        }

        if (xpSystem != null)
        {
            xpSystem.LevelUpTriggered -= HandleLevelChanged;
        }
    }

    public void RegisterKill(EnemyTier tier)
    {
        CurrentRun.Kills += tier switch
        {
            EnemyTier.Elite => 5,
            EnemyTier.Boss => 20,
            _ => 1
        };

        if (tier == EnemyTier.Boss)
        {
            CurrentRun.DefeatedBoss = true;
        }

        KillCountChanged?.Invoke(CurrentRun.Kills);
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        CurrentRun.GoldCollected += amount;
        GoldChanged?.Invoke(CurrentRun.GoldCollected);
    }

    private void HandleStateChanged(RunState state)
    {
        if (state != RunState.GameOver)
        {
            return;
        }

        metaProgression?.AddGold(CurrentRun.GoldCollected);
        RunEnded?.Invoke(CurrentRun);
    }

    private void HandleLevelChanged(int level)
    {
        CurrentRun.LevelReached = Mathf.Max(CurrentRun.LevelReached, level);
    }
}
