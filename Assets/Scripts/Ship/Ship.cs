using System;
using UnityEngine;

public class Ship : MonoBehaviour, IPlayerObject, IDamageable, IHealthObject
{
    [SerializeField] private ShipData _shipData;
    protected virtual ShipData shipData => _shipData;

    [SerializeField] protected ShipMovement shipMover;

    protected int currentShipHealth;

    public Action<Ship> OnShipDestroy;

    protected int _playerId;
    public virtual int playerId { get => _playerId; set => _playerId = value; }

    public virtual void TakeDamage(Damage damageInstance)
    {
        if (damageInstance.HasTarget(Damage.Target.Ship))
        {
            currentShipHealth = currentShipHealth - Math.Min(currentShipHealth, damageInstance.value);
            if (currentShipHealth <= 0)
            {
                OnShipDestroy?.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentShipHealth;
    }
    public int GetMaxHealth()
    {
        return shipData.maxHealth;
    }
    
    public virtual void SetDestination(Vector3 destination)
    {
        shipMover.SetDestination(destination);
    }
    public virtual void ResetDestination()
    {
        shipMover.ResetDestination();
    }

    public virtual void StopMoving()
    {
        shipMover.StopMoving();
    }

    public virtual void ResumeMovement()
    {
        shipMover.ResumeMovementToDestination();
    }

    public void Awake()
    {
        currentShipHealth = shipData.maxHealth;
        shipMover.speed = shipData.speed;
        shipMover.turningSpeed = shipData.turningSpeed;
    }
}
