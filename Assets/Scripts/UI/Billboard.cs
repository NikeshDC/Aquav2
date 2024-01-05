using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cameraTransform;

    public bool forwardFaceCamera;  //point forward vector towards camera

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.forward * (forwardFaceCamera ? -1 : 1));
    }
}
