using System;
using UnityEngine;

public abstract class BattleShip : Ship, IAmmoObject
{
    [SerializeField] private BattleShipData _battleShipData;
    public BattleShipData battleShipData => _battleShipData;
    protected override ShipData shipData => _battleShipData;

    protected int currentAmmo;
    protected int currentMenHealth;

    [SerializeField] private Weapon[] weapons;
    public int weaponCount => weapons.Length;
    public Weapon GetWeaponAt(int index) => weapons[index];

    public override int playerId 
    { 
        get => _playerId; 
        set {
                _playerId = value;
                foreach (Weapon weapon in weapons)
                {
                    weapon.playerId = _playerId;
                }
            }
    }

    public Action<BattleShip> OnMenDead;
    public Action<BattleShip> OnRestockAmmo;
    public Action<BattleShip> OnShipRepair;

    public override void TakeDamage(Damage damageInstance)
    {
        if (damageInstance.HasTarget(Damage.Target.Ship))
        {
            currentShipHealth -= Math.Min(currentShipHealth, damageInstance.value);
            if (currentShipHealth <= 0)
            {
                OnShipDestroy?.Invoke(this);
                Destroy(this.gameObject);
            }
        }
        if (damageInstance.HasTarget(Damage.Target.Men))
        {
            currentMenHealth -= Math.Min(currentMenHealth, damageInstance.value);
            if (currentMenHealth <= 0)
            {
                OnMenDead?.Invoke(this);
            }
        }
    }

    public int GetCurrentMenHealth()
    { return currentMenHealth; }
    public int GetMenMaxHealth()
    { return battleShipData.menMaxHealth; }

    public int GetMaxAmmo()
    {
        return battleShipData.maxAmmo;
    }
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
    public bool CanFire()
    {
        return currentAmmo > 0 && currentMenHealth > 0 && currentShipHealth > 0;
    }
    public void DecrementAmmo()
    {
        if (currentAmmo > 0) { currentAmmo--; }
    }

    public void RestockAmmo(int ammoAmount)
    {
        currentAmmo = Math.Min(currentAmmo + ammoAmount, battleShipData.maxAmmo);
        OnRestockAmmo?.Invoke(this);
    }
    public void HealMen(int healAmount)
    {
        currentMenHealth = Math.Min(currentMenHealth + healAmount, battleShipData.menMaxHealth);
    }
    public void RepairShip(int healAmount)
    {
        currentShipHealth = Math.Min(currentShipHealth + healAmount, battleShipData.maxHealth);
        OnShipRepair?.Invoke(this);
    }

    public new void Awake()
    {
        base.Awake();
        currentAmmo = _battleShipData.maxAmmo;
        currentMenHealth = _battleShipData.menMaxHealth;
    }
}
