using UnityEngine;

public abstract class Targetable : MonoBehaviour, IPlayerObject
{
    public abstract Vector3 location { get; }
    public abstract int playerId { get;}

    private Vector3 prevPosition;
    private Vector3 _velocity;
    public Vector3 velocity => _velocity;

    public void Start()
    {
        this.prevPosition = this.transform.position;
        this._velocity = Vector3.zero;
    }

    public void Update()
    {
        this._velocity = (this.transform.position - this.prevPosition) / Time.deltaTime;
        this.prevPosition = this.transform.position;
    }
}
