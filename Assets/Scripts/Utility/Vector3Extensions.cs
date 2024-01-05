using UnityEngine;

public static class Vector3Extensions
{ 
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3( x ?? vector.x, y ?? vector.y, z ?? vector.z); 
    }

    public static Vector3 Add(this Vector3 vector3, float x = 0f, float y = 0f, float z = 0f)
    {
        return new Vector3(vector3.x +x, vector3.y + y, vector3.z + z);
    }

    public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
    {
        float x  = vector.x < min.x ? min.x : (vector.x > max.x ? max.x : vector.x);
        float y = vector.y < min.y ? min.y : (vector.y > max.y ? max.y : vector.y);
        float z = vector.z < min.z ? min.z : (vector.z > max.z ? max.z : vector.z);

        return new Vector3(x, y, z);
    }

    public static Vector2 xy(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }
    public static Vector2 xz(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
    public static Vector2 yz(this Vector3 vector)
    {
        return new Vector2(vector.y, vector.z);
    }

    public static bool IsInRightSideOf(this Vector3 vector, Transform transform)
    {
        Vector3 directionVector = vector - transform.position;
        return Vector3.Angle(transform.right, directionVector) < 90f;
    }
    public static bool IsInLeftSideOf(this Vector3 vector, Transform transform)
    {
        Vector3 directionVector = vector - transform.position;
        return Vector3.Angle(-transform.right, directionVector) < 90f;
    }
    public static bool IsInRightSideOfWithinAngle(this Vector3 vector, Transform transform, float angle)
    {
        Vector3 directionVector = vector - transform.position;
        return Vector3.Angle(transform.right, directionVector) < angle;
    }
    public static bool IsInLeftSideOfWithinAngle(this Vector3 vector, Transform transform, float angle)
    {
        Vector3 directionVector = vector - transform.position;
        return Vector3.Angle(-transform.right, directionVector) < angle;
    }
}
