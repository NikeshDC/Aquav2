using UnityEngine;

public class Cannon : Weapon
{
    public float firingStrengthFactor = 5f;

    protected override void Fire(Vector3 targetLocation, Vector3 targetVelocity)
    {
        float speed = this.firingStrengthFactor * this.weaponData.firingStrength;

        Vector3 projectileVelocity = Vector3.zero;

        if (targetVelocity == Vector3.zero)
        {
            projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelStationaryTarget_HorizantalSpeedBased(launchPoint.xz(), targetLocation.xz(), speed);
        }
        else
        {
            projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelMovingTarget_HorizantalSpeedBased(launchPoint.xz(), targetLocation.xz(), targetVelocity.xz(),speed);
        }

        if (projectileVelocity == Vector3.zero)
        {
            Debug.LogWarning("Canon can't hit target at given speed");
            return;
        }

        //instantiate projectile and fire it
        GameObject canonProjectile= Instantiate(projectilePrefab, launchPoint, Quaternion.identity);
        if(canonProjectile.TryGetComponent<Cannonball>(out Cannonball canonball))
        {
            canonball.damage = this.weaponData.projectileDamage;
            canonball.Fire(projectileVelocity, this.playerId);
        }
        else
        {//only fire if the instance is a canonball
            Destroy(canonProjectile);
        }
    }
}