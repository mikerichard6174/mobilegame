using System.Collections.Generic;
using UnityEngine;

public class WeaponCatalog : MonoBehaviour
{
    [SerializeField] private List<WeaponDefinition> weaponDefinitions;

    public IReadOnlyList<WeaponDefinition> Weapons => weaponDefinitions;

    public void SetDefinitions(List<WeaponDefinition> definitions)
    {
        weaponDefinitions = definitions;
    }

    public WeaponDefinition GetById(string id)
    {
        return weaponDefinitions.Find(w => w != null && w.id == id);
    }

    public bool HasMinimumMvpLoadout()
    {
        return weaponDefinitions.Count >= 8;
    }
}
