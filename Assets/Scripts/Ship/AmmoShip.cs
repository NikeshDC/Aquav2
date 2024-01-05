using UnityEngine;

public class AmmoShip : SupplyShip
{
    protected override void ProvideSupplyToTarget()
    {
        targetShipToSupply.RestockAmmo(targetShipToSupply.GetMaxAmmo());
    }
}
