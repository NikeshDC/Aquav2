using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPlayerObject
{
    [SerializeField] protected WeaponData _weaponData;
    public WeaponData weaponData => _weaponData;
    [SerializeField] protected Transform _launchPoint;
    public Vector3 launchPoint => _launchPoint.position;
    [SerializeField] protected GameObject projectilePrefab;

    protected bool _isReloading = false;
    public bool isReloading => _isReloading;

    private int _playerId;
    public int playerId 
    { 
        get => _playerId;
        set 
        { 
            _playerId = value;
        }
    }

    public Action<Weapon> OnFireCallback;

    public virtual bool TryFire(Vector3 targetLocation, Vector3 targetVelocity)
    {
        if (isReloading)
        {
            return false;
        }
        this._isReloading = true;
        Reload();
        Fire(targetLocation, targetVelocity);
        OnFireCallback?.Invoke(this);
        return true;
    }
    protected abstract void Fire(Vector3 targetLocation, Vector3 targetVelocity);

    protected virtual void Reload()
    { StartCoroutine(C_Reload()); }
    private IEnumerator C_Reload()
    {
        yield return new WaitForSeconds(this.weaponData.reloadTime);
        _isReloading = false;
    }
}