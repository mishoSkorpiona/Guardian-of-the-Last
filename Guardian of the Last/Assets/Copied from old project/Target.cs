using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject rightLight;
    public GameObject wrongLight;

    public SAudioManager sam;

    LockedDoor lockedDoor;

    [HideInInspector]
    public float activeTime = float.NegativeInfinity;

    public bool doorOpened;

    public void SetDoor(LockedDoor ld)
    {
        lockedDoor = ld;
    }

    private void OnTriggerEnter(Collider other)
    {
        activeTime = Time.time;
        sam.Play("Click");
        lockedDoor.TargetHit();
        StartCoroutine(SetLight());
    }

    IEnumerator SetLight()
    {
        float currentActiveTime = activeTime;

        SetLight(true);
        
        yield return new WaitForSeconds(lockedDoor.timeBetweentargets);

        if (!doorOpened && activeTime == currentActiveTime)
        {
            SetLight(false);
        }
    }

    public void SetLight(bool right)
    {
        rightLight.SetActive(right);
        wrongLight.SetActive(!right);
    }
}
