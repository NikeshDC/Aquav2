using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    /*public BattleShipSidesBasedWeaponController controller;
    public BattleShip battleShip;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Ship targets:" + ShipTarget.targetsList.Count + ", Men targets:" + MenTarget.targetsList.Count);

        Debug.Log("Target detected: " + controller.targetDetected);
        if(controller.targetDetected)
        {
            Debug.Log("Primary side right: " + controller.PrimaryTargetSide_Right);
        }
        //Debug.Log("Ammo: " + battleShip.GetCurrentAmmo());
        //Debug.Log("CanFire: " + battleShip.CanFire());
    }*/


    public Transform target;
    public float turningSpeed;
    public float attackRange;
    public float attackAngleRange;

    public bool turnedToTarget;
    public float turnThreshold = 5f;

    private void Update()
    {
        if (!turnedToTarget)
        {
            Vector3 targetDirection = target.position - this.transform.position;
            float rightCrossTarget = Vector3.Cross(transform.right, targetDirection).y;
            float rotationDir = Mathf.Sign(rightCrossTarget);
            float rotationAmount = rotationDir * turningSpeed * Time.deltaTime;
            this.transform.Rotate(0f, rotationAmount, 0f);

            float angleToTarget = Mathf.Min(Vector3.Angle(transform.right, targetDirection), Vector3.Angle(-transform.right, targetDirection));
            Debug.Log(angleToTarget);

            if (angleToTarget < turnThreshold)
            {
                turnedToTarget = true;
            }
        }
        else
        {
            Debug.Log("TargetInLeftSide: " + IsTargetInRangeLeftSide(target.position));
            Debug.Log("TargetInRightSide: " + IsTargetInRangeRightSide(target.position));
            Debug.Log("Target in right side?: " + IsInRightSide(target.position));
        }
    }

    private bool IsTargetInRangeRightSide(Vector3 target)
    {
        return IsTargetInRange(target, 1f);
    }
    private bool IsTargetInRangeLeftSide(Vector3 target)
    {
        return IsTargetInRange(target, -1f);
    }
    private bool IsTargetInRange(Vector3 target, float side)
    {
        Vector3 targetDirection = target - this.transform.position;
        if (targetDirection.magnitude > attackRange ||
            Vector3.Angle(targetDirection, this.transform.right * side) > attackAngleRange)
        { return false; }

        return true;
    }

    private bool IsInRightSide(Vector3 target)
    {
        Vector3 targetDirection = target - this.transform.position;
        
        float rightSideWeaponTargetAngle = Vector2.Angle(transform.right.xz(), targetDirection.xz());
        float leftSideWeaponTargetAngle = Vector2.Angle(-transform.right.xz(), targetDirection.xz());

        if (rightSideWeaponTargetAngle < leftSideWeaponTargetAngle)
        {//target is on right side
            return true;
        }
        return false;
    }

}
