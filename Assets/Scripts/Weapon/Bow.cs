using UnityEngine;

public class Bow : Weapon
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
            projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelMovingTarget_HorizantalSpeedBased(launchPoint.xz(), targetLocation.xz(), targetVelocity.xz(), speed);
        }

        if(projectileVelocity == Vector3.zero)
        {
            Debug.LogWarning("Arrow can't hit target at given speed");
            return;
        }

        //instantiate projectile and fire it
        GameObject arrowProjectile = Instantiate(projectilePrefab, launchPoint, Quaternion.LookRotation(projectileVelocity));
        if (arrowProjectile.TryGetComponent<Arrow>(out Arrow arrow))
        {
            arrow.damage = this.weaponData.projectileDamage;
            arrow.Fire(projectileVelocity, this.playerId);
        }
        else
        {//only fire if the instance is a arrow
            Destroy(arrowProjectile);
        }
    }
}