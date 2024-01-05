using UnityEngine;

public class DamageTest : MonoBehaviour
{
    
    void Start()
    {
        Damage damage = new Damage(10, Damage.Target.Ship);

        Debug.Log(damage.HasTarget(Damage.Target.Men));
        Debug.Log(damage.HasTarget(Damage.Target.Ship));
    }
}
