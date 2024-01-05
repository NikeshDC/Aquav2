using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ProjectileRigidbody : Projectile
{
    protected Rigidbody rb;

    public new void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public override void Fire(Vector3 velocity, int playerId)
    {
        rb.isKinematic = false;
        rb.velocity = velocity;
        this.playerId = playerId;
    }
}
