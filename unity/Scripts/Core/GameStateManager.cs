using System;
using UnityEngine;

public enum RunState
{
    Menu,
    Running,
    Paused,
    LevelUp,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public RunState CurrentState { get; private set; } = RunState.Menu;

    public event Action<RunState> StateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartRun()
    {
        SetState(RunState.Running);
    }

    public void Pause()
    {
        if (CurrentState == RunState.Running)
        {
            SetState(RunState.Paused);
        }
    }

    public void Resume()
    {
        if (CurrentState == RunState.Paused || CurrentState == RunState.LevelUp)
        {
            SetState(RunState.Running);
        }
    }

    public void OpenLevelUp()
    {
        if (CurrentState == RunState.Running)
        {
            SetState(RunState.LevelUp);
        }
    }

    public void SetGameOver()
    {
        SetState(RunState.GameOver);
    }

    private void SetState(RunState nextState)
    {
        CurrentState = nextState;

        Time.timeScale = CurrentState == RunState.Running ? 1f : 0f;
        StateChanged?.Invoke(CurrentState);
    }
}
