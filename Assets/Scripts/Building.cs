using System;
using UnityEngine;

public class Building : MonoBehaviour, IPlayerObject, IDamageable, IHealthObject
{
    [SerializeField] private int _playerId;
    public int playerId => _playerId;

    [SerializeField] private int maxHealth;
    private int currentHealth;

    public int GetMaxHealth()
    { return maxHealth; }
    public int GetCurrentHealth()
    { return currentHealth; }

    public Action<Building> OnBuildingDestroyed;

    public void TakeDamage(Damage damageInstance)
    {
        if (damageInstance.HasTarget(Damage.Target.Ship))
        {
            currentHealth = currentHealth - Math.Min(currentHealth, damageInstance.value);
            if (currentHealth <= 0)
            {
                OnBuildingDestroyed?.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }
}
