using UnityEngine;

public class Gun : Weapon 
{ 
    public float firingStrengthFactor = 5f;

    protected override void Fire(Vector3 targetLocation, Vector3 targetVelocity)
    {
        float speed = this.firingStrengthFactor * this.weaponData.firingStrength;
        Vector3 projectileVelocity = WeaponTargetingSystem.GetVelocityForStationaryTarget_NoGravity(this.launchPoint, targetLocation, speed);
        //instantiate projectile and fire it
        GameObject bulletProjectile = Instantiate(projectilePrefab, launchPoint, Quaternion.LookRotation(projectileVelocity));
        if (bulletProjectile.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.damage = this.weaponData.projectileDamage;
            bullet.Fire(projectileVelocity, this.playerId);
        }
        else
        {//only fire if the instance is a bulet
            Destroy(bulletProjectile);
        }
    }
}