using UnityEngine;

public class Bullet : ProjectileShipOnlyTarget
{
    [SerializeField] private float afterImpactLifetime; //after a random of maximum of this lifetime gameobject is destroyed on impact

    private new void Awake()
    {
        base.Awake();
        rb.useGravity = false;
    }

    public override void OnImpact(IDamageable damageable, Vector3 impactPoint)
    {
        if (damageable != null)
        {
            damageable.TakeDamage(new Damage(this.damage, Damage.Target.Men));
        }
        Destroy(this.gameObject, Random.Range(0,this.afterImpactLifetime));  //destroy the projectile
    }
}
