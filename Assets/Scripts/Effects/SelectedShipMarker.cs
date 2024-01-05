using UnityEngine;

public class SelectedShipMarker : MonoBehaviour
{
    Ship previousSelectedShip;

    [SerializeField] private GameObject shipMarkerPrefab;
    [SerializeField] private GameObject shipMarker;

    private void DisableOutline(Ship ship)
    {
        if (shipMarker.transform.parent != ship.transform)
            return;

        shipMarker.transform.SetParent(null);
        shipMarker.SetActive(false);
    }

    private void AddOrEnableOutline(Ship ship)
    {
        shipMarker.transform.position = ship.transform.position;
        shipMarker.transform.SetParent(ship.transform);
        shipMarker.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(shipMarker == null)
        {
            shipMarker = Instantiate(shipMarkerPrefab);
        }

        Ship currentSelectedShip = GameManager.Instance.selectedShip;

        if (currentSelectedShip != previousSelectedShip)
        {
            if (currentSelectedShip != null)
            { AddOrEnableOutline(currentSelectedShip); }

            if (previousSelectedShip != null)
            { DisableOutline(previousSelectedShip); }
        }

        previousSelectedShip = currentSelectedShip;
    }
}
