using UnityEngine;

public class GunTest : MonoBehaviour
{
    public Gun gun;
    public KeyCode fireKey;

    public Transform target;

    public void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            gun.TryFire(target.position, Vector3.zero);
        }
    }
}
