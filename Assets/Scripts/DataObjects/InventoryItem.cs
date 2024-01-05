using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItemShip", fileName = "NewItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField] private int _id;
    public int id => _id;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private Sprite _icon;
    public Sprite icon => _icon;

    [SerializeField] private int _cost;
    public int cost => _cost;   
}
