using System.Collections.Generic;
using UnityEngine;

public class Cannonball : ProjectileShipOnlyTarget
{
    [SerializeField] private float impactRadius;

    public override void OnImpact(IDamageable primaryDamageable, Vector3 impactPoint)
    {
        Collider[] insideImpactColliders = Physics.OverlapSphere(impactPoint, impactRadius);
        List<IDamageable> damageables = new List<IDamageable>();

        foreach(Collider affectedCollider in insideImpactColliders)
        {
            IDamageable damageable = affectedCollider.transform.GetComponentInHeirarchy<IDamageable>();
            if(damageable != null)
            {//get every damageables inside the impact without repeating them
                if(!damageables.Contains(damageable))
                {
                    damageables.Add(damageable);
                }
            }
        }

        foreach(IDamageable damageable in damageables)
        {
            damageable.TakeDamage(new Damage(this.damage, Damage.Target.Men | Damage.Target.Ship));
        }

        Destroy(this.gameObject);
    }
}
