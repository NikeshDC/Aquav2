using System.Collections.Generic;
using UnityEngine;

public class MenTarget : Targetable
{
    public static List<MenTarget> targetsList = new List<MenTarget>();

    [SerializeField] private Transform colliderReference;
    public override Vector3 location => colliderReference.position;

    public BattleShip ship;
    public override int playerId => ship.playerId;

    private void Awake()
    {
        if (ship == null)
        { this.ship = GetComponentInParent<BattleShip>(); }
        this.ship.OnShipRepair += OnMenRestock;
    }

    private void OnEnable()
    {
        MenTarget.targetsList.Add(this);
        this.ship.OnShipDestroy += this.OnShipDestroyed;
        this.ship.OnMenDead += this.OnShipDestroyed;
    }

    private void OnDisable()
    {
        MenTarget.targetsList.Remove(this);
    }

    public void OnMenRestock(Ship ship)
    {
        this.enabled = true;
    }

    public void OnShipDestroyed(Ship ship)
    {
        this.ship.OnShipDestroy -= this.OnShipDestroyed;
        this.ship.OnMenDead -= this.OnShipDestroyed;
        this.enabled = false;
    }
}
