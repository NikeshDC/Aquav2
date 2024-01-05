using UnityEngine;

[RequireComponent(typeof(Ship))]
public class ShipDestroyedEffectPlayer : MonoBehaviour
{
    public GameObject effectPrefab;
    public float effectTime;
    [SerializeField] private Vector3 effectOffset; //offset from ship transform

    public GameObject destroyedShipVisual;


    void Start()
    { GetComponent<Ship>().OnShipDestroy += OnShipDestroyInstantiateEffect; }

    public void OnShipDestroyInstantiateEffect(Ship ship)
    {
        //instantiate effect
        Vector3 effectPoint = ship.transform.position + effectOffset;
        GameObject effectInstance = Instantiate(effectPrefab, effectPoint, Quaternion.identity);
        Destroy(effectInstance, effectTime);


        //replace gameObject with its visual only

        GameObject shipVisual = Instantiate(destroyedShipVisual, ship.transform.position, ship.transform.rotation);
        Destroy(shipVisual, effectTime);
    }
}
