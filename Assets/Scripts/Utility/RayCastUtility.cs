using System.Collections.Generic;
using UnityEngine;

public static class RayCastUtility
{
    public static T GetObjectUnderPointer<T>(Vector2 pointerPos, Camera eventCamera) where T : class
    {
        T objectUnderPointer = null;
        Ray ray = eventCamera.ScreenPointToRay(pointerPos);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            objectUnderPointer = hitInfo.transform.GetComponentInHeirarchy<T>();
        }
        return objectUnderPointer;
    }

    public static T[] GetObjectsInOverlappSphere<T>(Vector3 center, float radius) where T : class
    {
        List<T> objectInOverlappSphere = new List<T>();
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            T var = hitCollider.transform.GetComponentInHeirarchy<T>();
            if (objectInOverlappSphere != null)
            {
                objectInOverlappSphere.Add(var);
            }
        }
        return objectInOverlappSphere.ToArray();
    }
}
