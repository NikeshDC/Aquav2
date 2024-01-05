using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleShip))]
public abstract class BattleShipSidesBasedWeaponController : MonoBehaviour
{//must align sides with targets to attack the targets

    protected BattleShip battleShip;

    [SerializeField] protected bool autoSetWeaponsToSides;
    [SerializeField] protected Weapon[] leftSideWeapons;  //ship has left and right side canons that can only fire on their respective sides
    [SerializeField] protected Weapon[] rightSideWeapons;

    protected Targetable primaryTarget;
    private Weapon[] primaryTargetWeapons;
    protected Targetable secondaryTarget;
    private Weapon[] secondaryTargetWeapons;
    private bool primaryTargetSide_Right; //is primary target on right side

    public bool PrimaryTargetSide_Right => primaryTargetSide_Right;

    public bool turnToTarget;   //should the controller rotate object to face weapons to target
    public bool fireTarget;  //should the controller fire the target
    protected bool turnedToTarget;  //has the turning completed
    private bool turningToTarget;  //is the turning in progress
    protected float turnThreshold = 3f;  //should be less than targetRangeAngle
    private Coroutine turnCoroutine;
    [SerializeField] protected float targetRangeAngle = 10f;

    private bool _targetDetected;
    public bool targetDetected => _targetDetected;

    protected abstract Targetable GetNearestTargetInsideRadiusRange();
    protected abstract Targetable GetNearestTargetInsideRadiusRangeOnRightSide();
    protected abstract Targetable GetNearestTargetInsideRadiusRangeOnLeftSide();

    protected void Awake()
    {
        battleShip = GetComponent<BattleShip>();
        if(autoSetWeaponsToSides)
        {
            SetWeaponsSide();
        }
    }

    private void SetWeaponsSide()
    {
        List<Weapon> leftSideWeapons = new List<Weapon>();
        List<Weapon> rightSideWeapons = new List<Weapon>();
        int weaponCount = battleShip.weaponCount;
        for (int i=0; i< weaponCount; i++)
        {
            Weapon weapon = battleShip.GetWeaponAt(i);
            if (weapon.transform.position.IsInLeftSideOf(this.transform))
            {
                leftSideWeapons.Add(weapon);
            }
            else if(weapon.transform.position.IsInRightSideOf(this.transform))
            {
                rightSideWeapons.Add(weapon);
            }
        }
        this.leftSideWeapons = leftSideWeapons.ToArray();
        this.rightSideWeapons = rightSideWeapons.ToArray();
    }
    

    private void TurnToPrimaryTargetAndSetWeapons()
    {
        if (turnCoroutine != null)
        {
            StopCoroutine(turnCoroutine);
            turnCoroutine = null;
        }
        turnCoroutine = StartCoroutine(turnToTargetAndSetWeapons(this.primaryTarget));
    }
    private IEnumerator turnToTargetAndSetWeapons(Targetable targetToTurnTo)
    {
        turnedToTarget = false;
        turningToTarget = true;
        while (!turnedToTarget)
        {
            Vector3 targetDirection = targetToTurnTo.location - this.transform.position;
            float rightCrossTarget;

            if (targetToTurnTo.location.IsInRightSideOf(this.transform))
                { rightCrossTarget = Vector3.Cross(transform.right, targetDirection).y; }
            else
                { rightCrossTarget = Vector3.Cross(-transform.right, targetDirection).y; }

            float rotationDir = Mathf.Sign(rightCrossTarget);
            float rotationAmount = battleShip.battleShipData.turningSpeed * Time.deltaTime;
            rotationAmount = rotationDir * Mathf.Min(rotationAmount, rotationAmount * Mathf.Abs(rightCrossTarget));
            this.transform.Rotate(0f, rotationAmount, 0f);

            float angleToTarget = Mathf.Min(Vector2.Angle(transform.right.xz(), targetDirection.xz()), Vector2.Angle(-transform.right.xz(), targetDirection.xz()));

            if (angleToTarget < turnThreshold)
            {
                turnedToTarget = true;
            }
            yield return null;
        }
        turningToTarget = false;
        SetWeaponsToFire(targetToTurnTo);
    }

    private void SetWeaponsToFire(Targetable primaryTargetToFire)
    {//assumes turned to primary target
        if (primaryTargetToFire == null)
            return;

        Vector3 targetDirection = primaryTargetToFire.location - this.transform.position;
        if (targetDirection.magnitude > battleShip.battleShipData.attackRange) //not within firing range
            return;
        float rightSideWeaponTargetAngle = Vector2.Angle(transform.right.xz(), targetDirection.xz());
        float leftSideWeaponTargetAngle = Vector2.Angle(-transform.right.xz(), targetDirection.xz());
        
        if (rightSideWeaponTargetAngle < leftSideWeaponTargetAngle && rightSideWeaponTargetAngle <= targetRangeAngle)
        {//target is on right side
            primaryTargetSide_Right = true;
            primaryTarget = primaryTargetToFire;
            primaryTargetWeapons = rightSideWeapons;
            secondaryTarget = GetNearestTargetInsideRadiusRangeOnLeftSide();
            secondaryTargetWeapons = leftSideWeapons;
        }
        else if (leftSideWeaponTargetAngle < rightSideWeaponTargetAngle && leftSideWeaponTargetAngle <= targetRangeAngle)
        {//target on left side
            primaryTargetSide_Right = false;
            primaryTarget = primaryTargetToFire;
            primaryTargetWeapons = leftSideWeapons;
            secondaryTarget = GetNearestTargetInsideRadiusRangeOnRightSide();
            secondaryTargetWeapons = rightSideWeapons;
        }
    }

    private void FireConditionally()
    {
        if (turningToTarget)  //cant fire while turning to target
            return;

        if (battleShip.GetCurrentAmmo() > 0)
        {//has some ammo left
            if (primaryTarget != null)
            {//target has been set
                foreach (Weapon weapon in primaryTargetWeapons)
                {
                    if (battleShip.CanFire())
                    {
                        if (weapon.TryFire(primaryTarget.location, primaryTarget.velocity))
                        { battleShip.DecrementAmmo(); }
                    }
                    else
                    { break; }
                }
            }

            if (secondaryTarget != null)
            {//target has been set
                foreach (Weapon weapon in secondaryTargetWeapons)
                {
                    if (battleShip.CanFire())
                    {
                        if (weapon.TryFire(secondaryTarget.location, secondaryTarget.velocity))
                        { battleShip.DecrementAmmo(); }
                    }
                    else
                    { break; }
                }
            }
        }
    }

    protected virtual void CheckAndSetIfSetTargetsAreInRange()
    {
        if (primaryTarget != null)
        {
            if (primaryTargetSide_Right)
            {
                if (!IsTargetInRangeRightSide(primaryTarget))
                { primaryTarget = null; }
            }
            else
            {
                if (!IsTargetInRangeLeftSide(primaryTarget))
                { primaryTarget = null; }
            }
        }
        if (secondaryTarget != null)
        {
            if (primaryTargetSide_Right)
            {
                if (!IsTargetInRangeLeftSide(secondaryTarget))
                { secondaryTarget = null; }
            }
            else
            {
                if (!IsTargetInRangeRightSide(secondaryTarget))
                { secondaryTarget = null; }
            }
        }
    }
    private bool IsTargetInRangeRightSide(Targetable target)
    {
        return IsTargetInRange(target, 1f);
    }
    private bool IsTargetInRangeLeftSide(Targetable target)
    {
        return IsTargetInRange(target, -1f);
    }
    private bool IsTargetInRange(Targetable target, float side)
    {
        Vector3 targetDirection = target.location - this.transform.position;
        if (targetDirection.magnitude > battleShip.battleShipData.attackRange ||
            Vector3.Angle(targetDirection, this.transform.right * side) > this.targetRangeAngle)
        { return false; }

        return true;
    }

    protected void Update()
    {
        if (!turningToTarget)
        {
            CheckAndSetIfSetTargetsAreInRange();
        }

        if (primaryTarget == null && secondaryTarget == null)
        {
            _targetDetected = false;
            primaryTarget = GetNearestTargetInsideRadiusRange();
            if (primaryTarget != null)
            {
                _targetDetected = true;
                if (turnToTarget)
                {   TurnToPrimaryTargetAndSetWeapons(); }
            }
        }
        else if (primaryTarget == null && secondaryTarget != null)
        {
            primaryTarget = secondaryTarget;
            secondaryTarget = null;
        }
        else if (primaryTarget != null && secondaryTarget == null)
        {
            if (primaryTargetSide_Right)
            { secondaryTarget = GetNearestTargetInsideRadiusRangeOnLeftSide(); }
            else
            { secondaryTarget = GetNearestTargetInsideRadiusRangeOnRightSide(); }
        }

        if(fireTarget)
            FireConditionally();
    }
}
