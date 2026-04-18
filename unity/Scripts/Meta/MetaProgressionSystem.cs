using UnityEngine;

public class MetaProgressionSystem : MonoBehaviour
{
    private const string GoldKey = "meta_gold";
    private const string DamageNodeKey = "meta_node_damage";
    private const string HealthNodeKey = "meta_node_health";
    private const string MoveNodeKey = "meta_node_movespeed";

    [SerializeField] private PlayerStats playerStats;

    public int Gold => PlayerPrefs.GetInt(GoldKey, 0);

    private void Start()
    {
        ApplyUnlockedNodes();
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        PlayerPrefs.SetInt(GoldKey, Gold + amount);
    }

    public bool TryUnlockDamageNode()
    {
        return TryUnlockNode(DamageNodeKey, 150, () => playerStats.AddDamageMultiplier(0.1f));
    }

    public bool TryUnlockHealthNode()
    {
        return TryUnlockNode(HealthNodeKey, 120, () => playerStats.AddMaxHealth(20));
    }

    public bool TryUnlockMoveSpeedNode()
    {
        return TryUnlockNode(MoveNodeKey, 100, () => playerStats.AddMoveSpeed(0.08f));
    }

    private bool TryUnlockNode(string key, int cost, System.Action apply)
    {
        if (PlayerPrefs.GetInt(key, 0) == 1 || Gold < cost)
        {
            return false;
        }

        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.SetInt(GoldKey, Gold - cost);
        apply();
        return true;
    }

    private void ApplyUnlockedNodes()
    {
        if (PlayerPrefs.GetInt(DamageNodeKey, 0) == 1)
        {
            playerStats.AddDamageMultiplier(0.1f);
        }

        if (PlayerPrefs.GetInt(HealthNodeKey, 0) == 1)
        {
            playerStats.AddMaxHealth(20);
        }

        if (PlayerPrefs.GetInt(MoveNodeKey, 0) == 1)
        {
            playerStats.AddMoveSpeed(0.08f);
        }
    }
}
