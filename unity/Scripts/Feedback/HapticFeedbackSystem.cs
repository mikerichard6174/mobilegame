using UnityEngine;

public class HapticFeedbackSystem : MonoBehaviour
{
    public void TriggerLightImpact()
    {
#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }

    public void TriggerHeavyImpact()
    {
#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }
}
