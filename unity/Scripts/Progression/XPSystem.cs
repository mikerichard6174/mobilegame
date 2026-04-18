using System;
using UnityEngine;

public class XPSystem : MonoBehaviour
{
    [SerializeField] private int startingXpToLevel = 5;
    [SerializeField] private float xpGrowthPerLevel = 1.2f;

    public int CurrentLevel { get; private set; } = 1;
    public int CurrentXP { get; private set; }
    public int XPToNextLevel { get; private set; }

    public event Action<int> LevelUpTriggered;
    public event Action<int, int> XPChanged;

    private void Awake()
    {
        XPToNextLevel = Mathf.Max(1, startingXpToLevel);
        XPChanged?.Invoke(CurrentXP, XPToNextLevel);
    }

    public void AddXP(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        CurrentXP += amount;

        while (CurrentXP >= XPToNextLevel)
        {
            CurrentXP -= XPToNextLevel;
            LevelUp();
        }

        XPChanged?.Invoke(CurrentXP, XPToNextLevel);
    }

    private void LevelUp()
    {
        CurrentLevel++;
        XPToNextLevel = Mathf.Max(1, Mathf.RoundToInt(XPToNextLevel * xpGrowthPerLevel));

        LevelUpTriggered?.Invoke(CurrentLevel);
    }
}
