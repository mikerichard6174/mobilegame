using UnityEngine;

public class XPCollector : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var gem = other.GetComponent<XPGem>();
        if (gem == null)
        {
            return;
        }

        if (xpSystem != null)
        {
            xpSystem.AddXP(gem.Value);
        }

        Destroy(other.gameObject);
    }
}
