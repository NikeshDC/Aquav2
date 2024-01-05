using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshShipMovement : ShipMovement
{
    NavMeshAgent agent;

    Counter destinationReachedCallbackCounter = new Counter(1);

    public override bool destinationReached => AgentHasReachedDestination();

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        destination = transform.position;
    }

    protected override void MoveTo(Vector3 destination)
    {
        agent.speed = speed;
        agent.angularSpeed = turningSpeed;
        agent.SetDestination(destination);
    }

    public override void StopMoving()
    {
        agent.isStopped = true;
    }

    public override void ResumeMovementToDestination()
    {
        agent.isStopped = false;
    }

    private bool AgentHasReachedDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            return true;
        else
            return false;
    }

    public override void SetDestination(Vector3 destination)
    {
        base.SetDestination(destination);
        destinationReachedCallbackCounter.Reset();
    }
}
