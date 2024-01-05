using UnityEngine;

public abstract class SupplyShip : Ship
{//goes to targeted battle ship, supplies it and then moves to destination
    [SerializeField] private float supplyRadius;

    protected Vector3 returnPoint;
    public void SetReturnPoint(Vector3 returnPoint)
    {  this.returnPoint = returnPoint; }

    protected BattleShip targetShipToSupply;
    public void SetTargetToProvideSupply(BattleShip battleShip)
    { targetShipToSupply = battleShip; }

    protected virtual bool IsTargetShipInRange()
    {
        return (this.transform.position - targetShipToSupply.transform.position).magnitude <= supplyRadius;
    }

    protected abstract void ProvideSupplyToTarget();

    protected virtual void MoveToTargetShip()
    {
        shipMover.SetDestination(targetShipToSupply.transform.position);
    }

    protected virtual void ReturnToDestination()
    {
        shipMover.SetDestination(returnPoint);
    }

    public new void Awake()
    {
        base.Awake();
        returnPoint = transform.position;
    }

    public void Update()
    {
        if (targetShipToSupply != null) 
        {
            if (IsTargetShipInRange())
            {
                ProvideSupplyToTarget();
                ReturnToDestination();
                targetShipToSupply = null;
            }
            else
            {
                MoveToTargetShip();
            }
        }
    }
}
