using UnityEngine;

public abstract class BattleShipSidesBasedWeaponControllerShipTarget : BattleShipSidesBasedWeaponController
{
    protected override Targetable GetNearestTargetInsideRadiusRange()
    {
        float nearestTargetDistance = Mathf.Infinity;
        ShipTarget currentTarget = null;
        foreach (ShipTarget target in ShipTarget.targetsList)
        {
            if (target.playerId == this.battleShip.playerId) //skip same player target
            { continue; }

            float thisTargetDistance = Vector3.Distance(this.transform.position, target.location);
            if (thisTargetDistance <= battleShip.battleShipData.attackRange)
            {//target inside range
                if(currentTarget == null)
                {
                    currentTarget = target;
                }
                else if (target.priority > currentTarget.priority)
                {//higher priority target
                    currentTarget = target;
                }
                else if(target.priority == currentTarget.priority)
                {//perform distance check for same priority target
                    if (thisTargetDistance < nearestTargetDistance)
                    {
                        currentTarget = target;
                        nearestTargetDistance = thisTargetDistance;
                    }
                }
            }
        }
        return currentTarget;
    }

    protected override Targetable GetNearestTargetInsideRadiusRangeOnRightSide()
    {
        return GetNearestTargetInsideRadiusRangeOnSide(1f);
    }
    protected override Targetable GetNearestTargetInsideRadiusRangeOnLeftSide()
    {
        return GetNearestTargetInsideRadiusRangeOnSide(-1f);
    }
    private ShipTarget GetNearestTargetInsideRadiusRangeOnSide(float side)
    {//if side is +ve check on right else if -ve check on left
        float nearestTargetDistance = Mathf.Infinity;
        ShipTarget currentTarget = null;
        foreach (ShipTarget target in ShipTarget.targetsList)
        {
            if (target.playerId == this.battleShip.playerId) //skip same player target
            { continue; }
            Vector3 targetDirection = target.location - this.transform.position;
            float angleToTarget = Vector3.Angle(transform.right * side, targetDirection);
            if (angleToTarget <= targetRangeAngle)
            {//target inside angle range
                float thisTargetDistance = targetDirection.magnitude;
                if (thisTargetDistance <= battleShip.battleShipData.attackRange)
                {//target inside radius range
                    if (currentTarget == null)
                    {
                        currentTarget = target;
                    }
                    else if (target.priority > currentTarget.priority)
                    {//higher priority target
                        currentTarget = target;
                    }
                    else if (target.priority == currentTarget.priority)
                    {//perform distance check for same priority target
                        if (thisTargetDistance < nearestTargetDistance)
                        {
                            currentTarget = target;
                            nearestTargetDistance = thisTargetDistance;
                        }
                    }
                }
            }
        }

        return currentTarget;
    }

    protected override void CheckAndSetIfSetTargetsAreInRange()
    {
        base.CheckAndSetIfSetTargetsAreInRange();
        if(! ShipTarget.targetsList.Contains((ShipTarget) primaryTarget))
        {
            primaryTarget = null;
        }
        if (!ShipTarget.targetsList.Contains((ShipTarget)secondaryTarget))
        {
            secondaryTarget = null;
        }
    }
}
