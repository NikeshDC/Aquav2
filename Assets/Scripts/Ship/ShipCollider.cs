using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShipCollider : MonoBehaviour
{
    [SerializeField] private Ship shipOfCollider;
    public Ship ship => shipOfCollider;

    public int playerId => shipOfCollider.playerId;
}
