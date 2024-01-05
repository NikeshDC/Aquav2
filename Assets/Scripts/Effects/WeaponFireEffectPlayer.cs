using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponFireEffectPlayer : MonoBehaviour
{
    public GameObject effectPrefab;
    public float effectTime;

    void Start()
    { GetComponent<Weapon>().OnFireCallback += OnFireInstantiateEffect; }

    public void OnFireInstantiateEffect(Weapon weapon)
    {
        Vector3 firePoint = weapon.launchPoint;
        GameObject effectInstance = Instantiate(effectPrefab, firePoint, Quaternion.identity);
        Destroy(effectInstance, effectTime);
    }
}
