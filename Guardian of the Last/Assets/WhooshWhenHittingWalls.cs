using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhooshWhenHittingWalls : MonoBehaviour
{
    public SAudioManager am;

    private void OnTriggerEnter(Collider other)
    {
        am.Play("Whoosh");
    }
}
