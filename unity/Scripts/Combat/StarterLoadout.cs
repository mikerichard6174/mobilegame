using UnityEngine;

public class StarterLoadout : MonoBehaviour
{
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private WeaponDefinition guaranteedStarter;

    private void Start()
    {
        if (weaponSystem == null)
        {
            return;
        }

        var starter = guaranteedStarter != null ? guaranteedStarter : weaponSystem.GetRandomStarterWeapon();
        if (starter != null)
        {
            weaponSystem.TryAddOrUpgradeWeapon(starter);
        }
    }
}
