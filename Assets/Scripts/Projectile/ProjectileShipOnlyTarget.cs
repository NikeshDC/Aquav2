using UnityEngine;

public abstract class ProjectileShipOnlyTarget : ProjectileRigidbody
{
    protected sealed override void OnImpact(Collider collider, Collision collision) 
    {
        if (collider.TryGetComponent<ShipCollider>(out ShipCollider shipCollider))
        {//triggered with a ship's collider
            if (this.playerId != shipCollider.playerId)
            {
                IDamageable damageableShipObject = (IDamageable)shipCollider.ship;

                if (collision != null)
                {
                    OnImpact(damageableShipObject, collision.GetContact(0).point);
                }
                else
                {
                    OnImpact(damageableShipObject, this.transform.position);
                }
            }
        }
    }

    public abstract void OnImpact(IDamageable damageable, Vector3 impactPoint);
}
