using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 25;

    public int CurrentHealth { get; private set; }

    public event Action<Health> Died;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth > 0)
        {
            return;
        }

        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
