using UnityEngine;

public static class WeaponTargetingSystem
{
    public static Vector3 GetVelocityForSameLevelStationaryTarget_HorizantalSpeedBased(Vector2 source, Vector2 target, float horizantalSpeed)
    {
        return GetVelocityForSameLevelStationaryTarget_HorizantalSpeedBased(source, target, horizantalSpeed, - Physics.gravity.y);
    }
    public static Vector3 GetVelocityForSameLevelStationaryTarget_HorizantalSpeedBased(Vector2 source, Vector2 target, float horizantalSpeed, float verticalGravity)
    {//gives velocity vector required to reach target from source applying horizantal velocity along line joining source and destination and effect of vertical gravity in y-dir

        Vector2 directionVector = (target - source).normalized;
        float distanceToTarget = (target - source).magnitude;
        Vector2 horizantalVelocity = directionVector * horizantalSpeed;

        float timeToReachTarget = distanceToTarget / horizantalSpeed;
        float timeToReachMaxHeight = 0.5f * timeToReachTarget;

        float verticalSpeed = verticalGravity * timeToReachMaxHeight;

        return new Vector3(horizantalVelocity.x, verticalSpeed, horizantalVelocity.y);
    }

    public static Vector3 GetVelocityForSameLevelMovingTarget_HorizantalSpeedBased(Vector2 source, Vector2 target, Vector2 targetVelocity, float horizantalSpeed)
    {
        return GetVelocityForSameLevelMovingTarget_HorizantalSpeedBased(source, target, targetVelocity, horizantalSpeed, -Physics.gravity.y);
    }
    public static Vector3 GetVelocityForSameLevelMovingTarget_HorizantalSpeedBased(Vector2 source, Vector2 target, Vector2 targetVelocity, float horizantalSpeed, float verticalGravity)
    {//gives velocity vector required to reach moving target from source applying horizantal velocity along line joining source and destination and effect of vertical gravity in y-dir

        //predict target position in future and get velocity to intersect with that path (which resolves to solving quardatic equation below)
        Vector2 distanceVector = target - source;
        float a = targetVelocity.sqrMagnitude - horizantalSpeed * horizantalSpeed;
        float b = 2 * Vector2.Dot(distanceVector, targetVelocity);
        float c = distanceVector.sqrMagnitude;
        float discriminant = b * b - 4 * a * c;
        if(discriminant > 0)
        {
            float timeToReachTarget = (b + Mathf.Sqrt(discriminant)) / (2 * a);
            timeToReachTarget = Mathf.Max(timeToReachTarget, (b - Mathf.Sqrt(discriminant)) / (2 * a));
            float timeToReachMaxHeight = 0.5f * timeToReachTarget;

            float verticalSpeed = verticalGravity * timeToReachMaxHeight;
            float horizantalVelocityX = distanceVector.x / timeToReachTarget + targetVelocity.x;
            float horizantalVelocityY = distanceVector.y / timeToReachTarget + targetVelocity.y;

            return new Vector3(horizantalVelocityX, verticalSpeed, horizantalVelocityY);
        }
        else
        { 
            return Vector3.zero;
        }
    }

    public static Vector3 GetVelocityForSameLevelStationaryTarget_HeightBased(Vector2 source, Vector2 target, float height)
    {
        return GetVelocityForSameLevelStationaryTarget_HeightBased(source, target, height, -Physics.gravity.y);
    }
    public static Vector3 GetVelocityForSameLevelStationaryTarget_HeightBased(Vector2 source, Vector2 target, float height, float verticalGravity)
    {//gives velocity vector required to reach target from source reaching height given with effect of vertical gravity in y-dir

        float timeToReachMaxHeight = Mathf.Sqrt(2 * height / verticalGravity);
        float timeToReachTarget = 2 * timeToReachMaxHeight;

        Vector2 distanceVector = target - source;
        Vector2 horizantalVelocity = distanceVector / timeToReachTarget;
        float verticalSpeed = verticalGravity * timeToReachMaxHeight;

        return new Vector3(horizantalVelocity.x, verticalSpeed, horizantalVelocity.y);
    }

    public static Vector3 GetVelocityForSameLevelMovingTarget_HeightBased(Vector2 source, Vector2 target, Vector2 targetVelocity, float height)
    {
        return GetVelocityForSameLevelMovingTarget_HeightBased(source, target, targetVelocity, height, -Physics.gravity.y);
    }
    public static Vector3 GetVelocityForSameLevelMovingTarget_HeightBased(Vector2 source, Vector2 target, Vector2 targetVelocity, float height, float verticalGravity)
    {//gives velocity vector required to reach moving target from source reaching height given with effect of vertical gravity in y-dir

        float timeToReachMaxHeight = Mathf.Sqrt(2 * height / verticalGravity);
        float timeToReachTarget = 2 * timeToReachMaxHeight;

        Vector2 distanceVector = target - source;
        Vector2 horizantalVelocity = distanceVector / timeToReachTarget + targetVelocity;
        float verticalSpeed = verticalGravity * timeToReachMaxHeight;

        return new Vector3(horizantalVelocity.x, verticalSpeed, horizantalVelocity.y);
    }

    public static Vector3 GetVelocityForStationaryTarget_NoGravity(Vector3 source, Vector3 target, float speed)
    {
        Vector3 directionVector = (target - source).normalized;
        return directionVector * speed; 
    }
}
