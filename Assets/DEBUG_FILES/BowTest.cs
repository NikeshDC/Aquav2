using UnityEngine;

public class BowTest : MonoBehaviour
{
    public Bow bow;
    public KeyCode fireKey;

    public Transform target;

    public void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            bow.TryFire(target.position, Vector3.zero);
        }
    }
}
