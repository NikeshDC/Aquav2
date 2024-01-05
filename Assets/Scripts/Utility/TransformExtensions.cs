using UnityEngine;

public static class TransformExtensions
{
    public static T GetComponentInHeirarchy<T>(this Transform transform) where T : class
    {
        T componentObject = transform.GetComponentInParent<T>();
        if (componentObject == null)
        { componentObject = transform.GetComponentInChildren<T>(); }
        return componentObject;
    }
}
