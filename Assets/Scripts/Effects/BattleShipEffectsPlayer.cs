using UnityEngine;

[RequireComponent(typeof(BattleShip))]
public class BattleShipEffectsPlayer : MonoBehaviour
{
    public GameObject onRestockEffect_Prefab;
    public float onRestockEffect_Time;
    [SerializeField] private Vector3 onRestockEffect_Offset; //offset from ship transform

    public GameObject onShipRepairEffect_Prefab;
    public float onShipRepairEffect_Time;
    [SerializeField] private Vector3 onShipRepairEffect_Offset; //offset from ship transform

    void Start()
    { 
        GetComponent<BattleShip>().OnRestockAmmo += RestockAmmoInstantiateEffect;
        GetComponent<BattleShip>().OnShipRepair += ShipRepairInstantiateEffect;
    }

    public void RestockAmmoInstantiateEffect(BattleShip ship)
    {
        //instantiate effect
        Vector3 effectPoint = ship.transform.position + onRestockEffect_Offset;
        GameObject effectInstance = Instantiate(onRestockEffect_Prefab, effectPoint, Quaternion.identity);
        Destroy(effectInstance, onRestockEffect_Time);
    }

    public void ShipRepairInstantiateEffect(BattleShip ship)
    {
        //instantiate effect
        Vector3 effectPoint = ship.transform.position + onShipRepairEffect_Offset;
        GameObject effectInstance = Instantiate(onShipRepairEffect_Prefab, effectPoint, Quaternion.identity);
        Destroy(effectInstance, onShipRepairEffect_Time);
    }
}
