using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IPlayerObject
{
    public int damage;
    public int playerId { get; set; }

    public Action<Projectile> OnProjectileDestroy;

    private void OnTriggerEnter(Collider collider)
    {
        OnImpact(collider, null);
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnImpact(collision.collider, collision);
    }
    protected abstract void OnImpact(Collider collider, Collision collision);

    public abstract void Fire(Vector3 velocity, int playerId);

    public void Awake()
    {//hard limit on maximum lifetime of any projectile
        Destroy(gameObject, 10f);
    }

    protected void OnDestroy()
    {
        OnProjectileDestroy?.Invoke(this);
    }
}
