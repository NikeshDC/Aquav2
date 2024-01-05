using UnityEngine;

[CreateAssetMenu(menuName = "Ship", fileName = "NewShip")]
public class ShipData : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;    

    [SerializeField] private int _maxHealth;
    public int maxHealth => _maxHealth;

    [SerializeField] private float _speed;
    public float speed => _speed;

    [SerializeField] private float _turningSpeed;
    public float turningSpeed => _turningSpeed;
}
