using UnityEngine;

public class HealthBar : FillBar
{
    [SerializeField] IHealthObject healthObject;

    private int previousHealth;

    public void SetHealthObject(IHealthObject healthObject)
    {
        this.healthObject = healthObject;
        InitializeValues(); 
    }

    private void InitializeValues()
    {
        SetMaxValue(healthObject.GetMaxHealth());
        SetCurrentValue(healthObject.GetCurrentHealth());
        previousHealth = healthObject.GetCurrentHealth();
    }

    private void Start()
    {
        if (healthObject != null)
        { InitializeValues(); }
    }

    private void Update() 
    {
        if (healthObject == null)
            return;
        if (healthObject.GetCurrentHealth() != previousHealth)
        { SetCurrentValue(healthObject.GetCurrentHealth()); }
        previousHealth = healthObject.GetCurrentHealth();
    }
}
