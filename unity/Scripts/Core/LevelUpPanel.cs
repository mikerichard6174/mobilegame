using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private GameObject panelRoot;
    [SerializeField] private List<UpgradeOptionView> optionViews;

    private void Awake()
    {
        SetVisible(false);
    }

    private void OnEnable()
    {
        if (xpSystem != null)
        {
            xpSystem.LevelUpTriggered += HandleLevelUpTriggered;
        }
    }

    private void OnDisable()
    {
        if (xpSystem != null)
        {
            xpSystem.LevelUpTriggered -= HandleLevelUpTriggered;
        }
    }

    public void SelectUpgrade(UpgradeDefinition upgrade)
    {
        if (upgradeSystem != null)
        {
            upgradeSystem.ApplyUpgrade(upgrade);
        }

        SetVisible(false);
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.Resume();
        }
    }

    private void HandleLevelUpTriggered(int level)
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OpenLevelUp();
        }

        SetVisible(true);

        var options = upgradeSystem != null ? upgradeSystem.GenerateOptions(3) : new List<UpgradeDefinition>();

        for (var i = 0; i < optionViews.Count; i++)
        {
            var option = i < options.Count ? options[i] : null;
            optionViews[i].Bind(this, option);
        }
    }

    private void SetVisible(bool isVisible)
    {
        if (panelRoot != null)
        {
            panelRoot.SetActive(isVisible);
        }
    }
}
