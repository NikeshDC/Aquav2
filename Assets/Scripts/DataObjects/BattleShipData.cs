using UnityEngine;

[CreateAssetMenu(menuName = "BattleShip", fileName = "NewShip")]
public class BattleShipData : ShipData
{
    [SerializeField] private int _maxAmmo;
    public int maxAmmo => _maxAmmo;

    [SerializeField] private int _menMaxHealth;
    public int menMaxHealth => _menMaxHealth;

    [SerializeField] private float _attackRange;
    public float attackRange => _attackRange; 
}
