using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private GameObject panelRoot;
    [SerializeField] private List<Button> optionButtons;

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

    private void HandleLevelUpTriggered(int level)
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OpenLevelUp();
        }

        SetVisible(true);

        foreach (var optionButton in optionButtons)
        {
            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(SelectOption);
        }
    }

    private void SelectOption()
    {
        SetVisible(false);

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.Resume();
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
