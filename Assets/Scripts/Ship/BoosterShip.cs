using UnityEngine;

public class BoosterShip : SupplyShip
{
    protected override void ProvideSupplyToTarget()
    {
        targetShipToSupply.RepairShip(targetShipToSupply.GetMaxHealth());
        targetShipToSupply.HealMen(targetShipToSupply.GetMenMaxHealth());
    }
}
