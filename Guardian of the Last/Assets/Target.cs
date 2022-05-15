using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    LockedDoor lockedDoor;

    [HideInInspector]
    public float activeTime;

    private void OnCollisionEnter(Collision collision)
    {
        activeTime = Time.time;
        lockedDoor.TargetHit();
    }
}
