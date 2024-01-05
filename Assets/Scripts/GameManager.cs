using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Camera activeCamera;

    private Player player1 = new Player(1, 1000);
    private Player player2 = new Player(2, 1000);
    private Player currentActivePlayer;
    public Player activePlayer => currentActivePlayer;

    [SerializeField] private SerializedDictionary<InventoryItem, GameObject> InventoryItemIdToShipMap;
    private Dictionary<InventoryItem, GameObject> inventoryItemIdToShipMap;

    private Ship currentSelectedShip;
    public Ship selectedShip => currentSelectedShip;

    [SerializeField] private RectTransform touchArea;

    [SerializeField] private CameraZoom zoomManager;
    [SerializeField] private CameraPanning panManager;
    private Coroutine panZoomCheckRoutine;

    public void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        { Destroy(this); }

        currentActivePlayer = player1;
        activeCamera = Camera.main;
        SetInventoryToShipMap();
    }

    private void SetInventoryToShipMap()
    {
        inventoryItemIdToShipMap = InventoryItemIdToShipMap;
    }

    public void TogglePlayer()
    {
        if(currentActivePlayer == player1)
        { currentActivePlayer = player2; }
        else
        { currentActivePlayer = player1; }
    }

    public void TryBuyInventoryItemAndPlaceToWorld(InventoryItem item, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = null;
        if (inventoryItemIdToShipMap.TryGetValue(item, out objectToSpawn))
        {//has mapping to object
            if (currentActivePlayer.TryDecrementPoints(item.cost))
            {//player can buy the item
                GameObject spawnedShip = Instantiate(objectToSpawn, position, rotation);
                Ship ship = spawnedShip.GetComponent<Ship>();
                ship.playerId = activePlayer.playerId;
            }
        }
    }

    private void MoveOrSelectNewShipOnTap()
    {
        Ship newSelectedShip = null;
        Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            newSelectedShip = hitInfo.transform.GetComponentInParent<Ship>();
            if(newSelectedShip == currentSelectedShip)
            {
                currentSelectedShip = null;
            }
            else if (newSelectedShip != null)
            {//got a ship target
                if (newSelectedShip.playerId != activePlayer.playerId)
                    return;

                if(currentSelectedShip != null && currentSelectedShip is SupplyShip && newSelectedShip is BattleShip)
                {
                    if (currentSelectedShip.playerId == newSelectedShip.playerId)
                    {
                        ((SupplyShip)currentSelectedShip).SetTargetToProvideSupply((BattleShip)newSelectedShip);
                    }
                }
                else
                {
                    currentSelectedShip = newSelectedShip;
                }
            }
            else if(currentSelectedShip != null)
            {//not a ship target
                currentSelectedShip.SetDestination(hitInfo.point);
                if(currentSelectedShip is SupplyShip)
                {
                    ((SupplyShip)currentSelectedShip).SetReturnPoint(hitInfo.point);
                }
            }
        }
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(touchArea, Input.mousePosition))
        {
            if(panZoomCheckRoutine != null)
            { StopCoroutine(panZoomCheckRoutine); }
            panZoomCheckRoutine = StartCoroutine(C_PerformPanCheckAndMoveSelectShip());
        }
    }

    private IEnumerator C_PerformPanCheckAndMoveSelectShip()
    {
        yield return new WaitForSeconds(0.1f);
        if(!panManager.IsPanning() && !zoomManager.IsZooming())
        {
            MoveOrSelectNewShipOnTap();
        }
    }
}
