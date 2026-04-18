using UnityEngine;

public class UnlockSystem : MonoBehaviour
{
    private const string HeroTwoUnlockedKey = "unlock_hero_2";
    private const string WeaponArcBurstUnlockedKey = "unlock_weapon_arc_burst";

    [SerializeField] private RunSummaryManager runSummary;

    public bool IsHeroTwoUnlocked => PlayerPrefs.GetInt(HeroTwoUnlockedKey, 0) == 1;
    public bool IsArcBurstUnlocked => PlayerPrefs.GetInt(WeaponArcBurstUnlockedKey, 0) == 1;

    private void OnEnable()
    {
        if (runSummary != null)
        {
            runSummary.RunEnded += HandleRunEnded;
        }
    }

    private void OnDisable()
    {
        if (runSummary != null)
        {
            runSummary.RunEnded -= HandleRunEnded;
        }
    }

    private void HandleRunEnded(RunSummaryData data)
    {
        if (data.SurvivalTimeSeconds >= 600f)
        {
            PlayerPrefs.SetInt(HeroTwoUnlockedKey, 1);
        }

        if (data.Kills >= 500)
        {
            PlayerPrefs.SetInt(WeaponArcBurstUnlockedKey, 1);
        }
    }
}
