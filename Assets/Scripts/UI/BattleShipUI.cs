using UnityEngine;
using UnityEngine.UI;

public class BattleShipUI : MonoBehaviour
{
    [SerializeField] FillBar shipHealthBar;
    [SerializeField] FillBar ammoBar;
    [SerializeField] FillBar menHealthBar;
    [SerializeField] Text playerTag;

    [SerializeField] BattleShip battleShip;

    private void SetCurrentValues()
    {
        shipHealthBar.SetCurrentValue(battleShip.GetCurrentHealth());
        ammoBar.SetCurrentValue(battleShip.GetCurrentAmmo());
        menHealthBar.SetCurrentValue(battleShip.GetCurrentMenHealth());
    }

    private void Start()
    {
        shipHealthBar.SetMaxValue(battleShip.GetMaxHealth());
        ammoBar.SetMaxValue(battleShip.GetMaxAmmo());
        menHealthBar.SetMaxValue(battleShip.GetMenMaxHealth());
        playerTag.text = "P" + battleShip.playerId;

        SetCurrentValues();
    }

    private void Update()
    {
        SetCurrentValues();
    }
}
