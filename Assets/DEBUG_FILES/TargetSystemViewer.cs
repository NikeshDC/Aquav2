using UnityEngine;

[RequireComponent(typeof(PathRenderer))]
public class TargetSystemViewer : MonoBehaviour
{
    public bool heightBased;

    public float speed;
    public float gravity;
    public float height;

    public Transform source;
    public Transform target;
    public Vector3 targetVelocity;
    private Rigidbody targetRb;

    PathRenderer pathRenderer;

    public GameObject projectileObject;
    public float projectileLifetime;
    public KeyCode fireButton;

    private void Start()
    {
        pathRenderer = GetComponent<PathRenderer>();
        targetRb = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 projectileVelocity;

        if(targetRb)
        {
            targetRb.velocity = targetVelocity;
        }

        if (targetRb && targetRb.velocity != Vector3.zero)
        {
            if (!heightBased)
            {
                projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelMovingTarget_HorizantalSpeedBased(source.position.xz(), target.position.xz(), targetRb.velocity.xz(), speed);
            }
            else
            {
                projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelMovingTarget_HeightBased(source.position.xz(), target.position.xz(), targetRb.velocity.xz(), height);
            }
        }
        else
        {
            if (!heightBased)
            {
                projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelStationaryTarget_HorizantalSpeedBased(source.position.xz(), target.position.xz(), speed);
            }
            else
            {
                projectileVelocity = WeaponTargetingSystem.GetVelocityForSameLevelStationaryTarget_HeightBased(source.position.xz(), target.position.xz(), height);
            }
        }

        if (projectileVelocity != Vector3.zero)
        {
            pathRenderer.DrawPathUnderGravity(source.position, projectileVelocity, gravity, 0f, 0.01f);

            if (Input.GetKeyDown(fireButton) && projectileObject != null)
            {
                GameObject projectileInstance = Instantiate(projectileObject, source.position, Quaternion.identity);
                projectileInstance.GetComponent<Rigidbody>().velocity = projectileVelocity;
                Destroy(projectileInstance, projectileLifetime);
            }
        }
    }
}
