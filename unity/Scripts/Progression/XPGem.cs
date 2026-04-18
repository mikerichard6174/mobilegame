using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class XPGem : MonoBehaviour
{
    [SerializeField] private int xpValue = 1;

    public int Value => xpValue;

    public void SetValue(int value)
    {
        xpValue = Mathf.Max(1, value);
    }

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }
}
