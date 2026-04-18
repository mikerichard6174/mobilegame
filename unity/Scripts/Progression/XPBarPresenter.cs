using UnityEngine;
using UnityEngine.UI;

public class XPBarPresenter : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private Slider xpSlider;

    private void OnEnable()
    {
        if (xpSystem != null)
        {
            xpSystem.XPChanged += HandleXPChanged;
            HandleXPChanged(xpSystem.CurrentXP, xpSystem.XPToNextLevel);
        }
    }

    private void OnDisable()
    {
        if (xpSystem != null)
        {
            xpSystem.XPChanged -= HandleXPChanged;
        }
    }

    private void HandleXPChanged(int currentXP, int xpToNextLevel)
    {
        if (xpSlider == null)
        {
            return;
        }

        xpSlider.maxValue = xpToNextLevel;
        xpSlider.value = currentXP;
    }
}
