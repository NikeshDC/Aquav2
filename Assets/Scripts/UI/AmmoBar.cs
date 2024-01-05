using UnityEngine;

public class AmmoBar : FillBar
{
    [SerializeField] IAmmoObject ammoObject;

    private void Start()
    {
       SetMaxValue(ammoObject.GetMaxAmmo());
       SetCurrentValue(ammoObject.GetCurrentAmmo());
    }

    private void Update()
    {
        SetCurrentValue(ammoObject.GetCurrentAmmo());
    }
}
