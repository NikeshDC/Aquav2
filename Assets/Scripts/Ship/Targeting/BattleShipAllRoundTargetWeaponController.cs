using UnityEngine;

[RequireComponent(typeof(BattleShip))]
public abstract class BattleShipAllRoundTargetWeaponController : MonoBehaviour
{//must align sides with targets to attack the targets

    protected BattleShip battleShip;

    protected Targetable target;

    private bool _targetDetected;
    public bool targetDetected => _targetDetected;

    public bool fireTarget;

    protected abstract Targetable GetNearestTargetInsideRadiusRange();

    protected void Awake()
    {
        battleShip = GetComponent<BattleShip>();
    }

    private void FireConditionally()
    {
        if (battleShip.GetCurrentAmmo() > 0 && target != null)
        {//has some ammo left
            for(int i=0; i < battleShip.weaponCount; i++)
            {
                if (battleShip.CanFire())
                {
                    Weapon weapon = battleShip.GetWeaponAt(i);
                    if (weapon.TryFire(target.location, target.velocity))
                    { battleShip.DecrementAmmo(); }
                }
                else
                { break; }
            }
        }
    }

    protected virtual void CheckAndSetIfSetTargetIsInRange()
    {
        if (target != null)
        {
            if (!IsTargetInRange(target))
                target = null;
        }
    }
  
    private bool IsTargetInRange(Targetable target)
    {
        Vector3 targetDirection = target.location - this.transform.position;
        if (targetDirection.magnitude > battleShip.battleShipData.attackRange)
        { return false; }

        return true;
    }

    protected void Update()
    {
        CheckAndSetIfSetTargetIsInRange();

        if (target == null)
        {
            _targetDetected = false;
            target = GetNearestTargetInsideRadiusRange();
            if (target != null)
            {
                _targetDetected = true;
            }
        }

        if (fireTarget)
        { FireConditionally(); }
    }
}
