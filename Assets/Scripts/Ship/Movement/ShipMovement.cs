using UnityEngine;
using UnityEngine.Events;

public abstract class ShipMovement : MonoBehaviour
{
    protected Vector3 destination;
    protected bool destinationIsSet = false;
    public abstract bool destinationReached { get; }

    public float speed;
    public float turningSpeed;

    protected abstract void MoveTo(Vector3 destination);
    public abstract void StopMoving();

    public virtual void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationIsSet = true;
        MoveTo(destination);
    }
    public virtual void ResetDestination()
    {
        destinationIsSet = false;
    }
    public virtual void ResumeMovementToDestination()
    {
        if (destinationIsSet && !destinationReached)
        {
            MoveTo(destination);
        }
    }
}