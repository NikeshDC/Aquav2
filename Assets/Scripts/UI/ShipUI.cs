using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    [SerializeField] FillBar shipHealthBar;
    [SerializeField] Text playerTag;

    [SerializeField] Ship ship;

    private void SetCurrentValues()
    {
        shipHealthBar.SetCurrentValue(ship.GetCurrentHealth());
        playerTag.text = "P" + ship.playerId;
    }

    private void Start()
    {
        shipHealthBar.SetMaxValue(ship.GetMaxHealth());
        SetCurrentValues();
    }

    private void Update()
    {
        SetCurrentValues();
    }
}
