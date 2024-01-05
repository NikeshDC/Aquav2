using UnityEngine;

public class ShipMovementTest : MonoBehaviour
{
    [SerializeField] ShipMovement shipMover;
    [SerializeField] private float speed;
    [SerializeField] private float turningSpeed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                shipMover.speed = speed;
                shipMover.turningSpeed = turningSpeed;
                shipMover.SetDestination(hit.point);
            }
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            shipMover.StopMoving();
        }
    }

    public void OnShipDestinationReached()
    {
        Debug.Log("destination reached");
    }
}
