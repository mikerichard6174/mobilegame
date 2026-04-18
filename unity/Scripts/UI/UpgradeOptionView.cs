using UnityEngine;
using UnityEngine.UI;

public class UpgradeOptionView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;

    private UpgradeDefinition currentUpgrade;
    private LevelUpPanel panel;

    public void Bind(LevelUpPanel ownerPanel, UpgradeDefinition upgrade)
    {
        panel = ownerPanel;
        currentUpgrade = upgrade;

        if (titleText != null)
        {
            titleText.text = upgrade != null ? upgrade.displayName : "N/A";
        }

        if (descriptionText != null)
        {
            descriptionText.text = upgrade != null ? upgrade.description : "No upgrade.";
        }

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Apply);
            button.interactable = upgrade != null;
        }
    }

    private void Apply()
    {
        if (panel != null && currentUpgrade != null)
        {
            panel.SelectUpgrade(currentUpgrade);
        }
    }
}
