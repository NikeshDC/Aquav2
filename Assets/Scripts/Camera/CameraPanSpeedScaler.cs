using UnityEngine;

public class CameraPanSpeedScaler : MonoBehaviour
{
    [SerializeField] private CameraPanning cameraPanner;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float scaleFactor;
    [SerializeField] private float baseDistance;
    [SerializeField] private float baseSpeed;

    public void Update()
    {
        cameraPanner.panSpeed = Mathf.Pow((mainCamera.transform.position.y / baseDistance), scaleFactor) * baseSpeed;
    }
}
