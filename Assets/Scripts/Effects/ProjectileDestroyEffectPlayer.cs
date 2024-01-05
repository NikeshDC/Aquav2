using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class ProjectileDestroyEffectPlayer : MonoBehaviour
{
    public GameObject effectPrefab;
    public float effectTime;

    void Start()
    {   GetComponent<Projectile>().OnProjectileDestroy += OnDestroyInstantiateEffect; }

    public void OnDestroyInstantiateEffect(Projectile projectile)
    {   
        Vector3 destroyPoint = projectile.transform.position;
        GameObject effectInstance = Instantiate(effectPrefab, destroyPoint, projectile.transform.rotation);
        Destroy(effectInstance, effectTime);
    }
}
