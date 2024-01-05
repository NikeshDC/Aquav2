using UnityEngine;

//[RequireComponent(typeof(IHealthObject))]
public class HealthLog : MonoBehaviour
{
    public IHealthObject healthObject;

    //public BattleShip battleShip;

    private void Awake()
    {
        healthObject = GetComponent<IHealthObject>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Ship Health: " + healthObject.GetCurrentHealth() );
            //Debug.Log("Men health: " + battleShip.GetCurrentMenHealth());
        }
    }
}
