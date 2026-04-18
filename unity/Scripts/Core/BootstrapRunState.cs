using UnityEngine;

public class BootstrapRunState : MonoBehaviour
{
    private void Start()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.StartRun();
        }
    }
}
