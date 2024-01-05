using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField] private InventoryItem itemDetails;
    public InventoryItem item => itemDetails;

    [SerializeField] Text itemName;
    [SerializeField] Image itemIcon;
    [SerializeField] Text itemCost;

    private void Start()
    {
        itemName.text = itemDetails.Name;
        itemIcon.sprite = itemDetails.icon;
        itemCost.text = itemDetails.cost.ToString();
    }

}
