using UnityEngine;

public class Mortar : Weapon
{
    public float firingStrengthFactor = 5f;

    protected override void Fire(Vector3 targetLocation, Vector3 targetVelocity)
    {
        float height = this.firingStrengthFactor * this.weaponData.firingStrength;

        Vector3 projectileVelocity = Vector3.zero;

        if (targetVelocity == Vector3.zero)
        {
            projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelStationaryTarget_HeightBased(launchPoint.xz(), targetLocation.xz(), height);
        }
        else
        {
            projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelMovingTarget_HeightBased(launchPoint.xz(), targetLocation.xz(), targetVelocity.xz(), height);
        }


        //instantiate projectile and fire it
        GameObject mortarProjectile = Instantiate(projectilePrefab, launchPoint, Quaternion.identity);
        if (mortarProjectile.TryGetComponent<Cannonball>(out Cannonball canonball))
        {
            canonball.damage = this.weaponData.projectileDamage;
            canonball.Fire(projectileVelocity, this.playerId);
        }
        else
        {//only fire if the instance is a canonball
            Destroy(mortarProjectile);
        }
    }
}