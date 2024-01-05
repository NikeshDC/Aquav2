using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryItemDisplay))]
public class InventoryItemDragObjectSpawner : DraggingTriggerer, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    public GameObject dragObjectPrefab;
    GameObject dragObject;

    bool isDragging = false;

    InventoryItem item;

    private void Awake()
    {
        item = GetComponent<InventoryItemDisplay>().item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragObject.GetComponent<SpawnObjectPlaceholder>().CanSpawn())
        {
            GameManager.Instance.TryBuyInventoryItemAndPlaceToWorld(item, dragObject.transform.position, dragObject.transform.rotation);
        }

        isDragging = false;
        Destroy(dragObject);
    }

    public override bool IsDragging()
    {
        return isDragging;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging)
        {
            dragObject = Instantiate(dragObjectPrefab);
            dragObject.GetComponent<DraggableObject>().followPointerPosition = true;
        }
    }
}
