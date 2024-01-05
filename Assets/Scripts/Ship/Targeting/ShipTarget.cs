using System.Collections.Generic;
using UnityEngine;

public class ShipTarget : Targetable
{
    public static List<ShipTarget> targetsList = new List<ShipTarget>();

    [SerializeField] private int _priority;
    public int priority => _priority;

    [SerializeField] private Transform colliderReference;
    public override Vector3 location => colliderReference.position;

    public Ship ship;
    public override int playerId => ship.playerId;

    private void Awake()
    {
        if (ship == null)
        { this.ship = GetComponentInParent<Ship>(); }
    }

    private void OnEnable()
    {
        ShipTarget.targetsList.Add(this);
        this.ship.OnShipDestroy += this.OnShipDestroyed;
    }

    private void OnDisable()
    {
        ShipTarget.targetsList.Remove(this);
    }

    public void OnShipDestroyed(Ship ship)
    {
        this.ship.OnShipDestroy -= this.OnShipDestroyed;
        this.enabled = false;
    }
}
