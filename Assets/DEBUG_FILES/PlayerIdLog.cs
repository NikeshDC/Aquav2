using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IPlayerObject))]
public class PlayerIdLog : MonoBehaviour
{
    IPlayerObject playerObject;

    public void Start()
    {
        playerObject = GetComponent<IPlayerObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("playerId: " + playerObject.playerId);
    }
}
