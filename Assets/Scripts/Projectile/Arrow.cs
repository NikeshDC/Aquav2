using UnityEngine;

public class Arrow : ProjectileShipOnlyTarget
{
    [SerializeField] private float afterImpactLifetime; //after a random of maximum of this lifetime gameobject is destroyed on impact
    
    public override void OnImpact(IDamageable damageable, Vector3 impactPoint)
    {
        if (damageable != null)
        {
            damageable.TakeDamage(new Damage(this.damage, Damage.Target.Men));
        }
        Destroy(this.gameObject, Random.Range(0, this.afterImpactLifetime));  //destroy array projectile
    }

    public void Update()
    {
        //align arrow to its movement
        this.transform.LookAt(this.transform.position + rb.velocity);
    }
}
