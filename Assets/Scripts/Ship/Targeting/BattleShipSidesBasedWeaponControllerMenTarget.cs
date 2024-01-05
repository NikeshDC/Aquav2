using UnityEngine;

public abstract class BattleShipSidesBasedWeaponControllerMenTarget : BattleShipSidesBasedWeaponController
{
    protected override Targetable GetNearestTargetInsideRadiusRange()
    {
        float nearestTargetDistance = Mathf.Infinity;
        MenTarget newTarget = null;
        foreach (MenTarget target in MenTarget.targetsList)
        {
            if (target.playerId == this.battleShip.playerId) //skip same player target
            {   continue; }
            float currentTargetDistance = Vector3.Distance(this.transform.position, target.location);
            if (currentTargetDistance <= battleShip.battleShipData.attackRange)
            {//target inside range
                if (currentTargetDistance < nearestTargetDistance)
                {
                    newTarget = target;
                    nearestTargetDistance = currentTargetDistance;
                }
            }
        }
        return newTarget;
    }

    protected override Targetable GetNearestTargetInsideRadiusRangeOnRightSide()
    {
        return GetNearestTargetInsideRadiusRangeOnSide(1f);
    }
    protected override Targetable GetNearestTargetInsideRadiusRangeOnLeftSide()
    {
        return GetNearestTargetInsideRadiusRangeOnSide(-1f);
    }
    private MenTarget GetNearestTargetInsideRadiusRangeOnSide(float side)
    {//if side is +ve check on right else if -ve check on left
        float nearestTargetDistance = Mathf.Infinity;
        MenTarget sideTarget = null;
        foreach (MenTarget target in MenTarget.targetsList)
        {
            if (target.playerId == this.battleShip.playerId) //skip same player target
            { continue; }
            Vector3 targetDirection = target.location - this.transform.position;
            float angleToTarget = Vector3.Angle(transform.right * side, targetDirection);
            if (angleToTarget <= targetRangeAngle)
            {//target inside angle range
                float currentTargetDistance = targetDirection.magnitude;
                if (currentTargetDistance <= battleShip.battleShipData.attackRange)
                {//target inside radius range
                    if (currentTargetDistance < nearestTargetDistance)
                    {
                        sideTarget = target;
                        nearestTargetDistance = currentTargetDistance;
                    }
                }
            }
        }

        return sideTarget;
    }

    protected override void CheckAndSetIfSetTargetsAreInRange()
    {
        base.CheckAndSetIfSetTargetsAreInRange();
        if (!MenTarget.targetsList.Contains((MenTarget)primaryTarget))
        {
            primaryTarget = null;
        }
        if (!MenTarget.targetsList.Contains((MenTarget)secondaryTarget))
        {
            secondaryTarget = null;
        }
    }
}
