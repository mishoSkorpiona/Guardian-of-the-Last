using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public Target[] targets;
    public float timeBetweentargets;

    public void TargetHit()
    {
        foreach (Target t in targets)
        {
            if (t.activeTime < Time.time - timeBetweentargets)
            {
                break;
            }
        }

        //open the door
        Debug.Log("Door opened!");
    }
}
