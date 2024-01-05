using UnityEngine;

[RequireComponent(typeof(MortarShip))]
public class MortarShipWeaponController : BattleShipAllRoundTargetWeaponController
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
        return currentTarget;
    }

    protected override void CheckAndSetIfSetTargetIsInRange()
    {
        base.CheckAndSetIfSetTargetIsInRange();
        if (!ShipTarget.targetsList.Contains((ShipTarget)target))
        {
            target = null;
        }
    }
}

