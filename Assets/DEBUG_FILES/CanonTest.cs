using UnityEngine;

public class CanonTest : MonoBehaviour
{
    public Cannon canon;
    public KeyCode fireKey;

    public Transform target;

    public void Update()
    {
        if(Input.GetKeyDown(fireKey))
        {
            canon.TryFire(target.position, Vector3.zero);
        }
    }
}
