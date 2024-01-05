using UnityEngine;

public class ShipPlayerIdSetter : MonoBehaviour
{
    public int playerId;

    public Ship ship;

    // Update is called once per frame
    void Update()
    {
        ship.playerId = playerId;
    }
}
