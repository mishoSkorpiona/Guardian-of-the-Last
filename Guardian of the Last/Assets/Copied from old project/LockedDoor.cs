using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedDoor : MonoBehaviour
{
    public SAudioManager sam;
    public Target[] targets;
    public float timeBetweentargets;

    private void Start()
    {
        foreach (Target t in targets)
        {
            t.SetDoor(this);
            t.activeTime = -timeBetweentargets;
        }
    }

    public void TargetHit()
    {
        Debug.Log(Time.time);

        foreach (Target t in targets)
        {
            Debug.Log(t.activeTime);

            if (t.activeTime < Time.time - timeBetweentargets)
            {
                Debug.Log("Outside of range");
                return;
            }
        }

        //open the door
        Debug.Log("Door opened!");

        sam.Play("Open");

        foreach (Target t in targets)
        {
            t.doorOpened = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (Target t in targets)
        {
            if (!t.doorOpened)
            {
                return;
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
