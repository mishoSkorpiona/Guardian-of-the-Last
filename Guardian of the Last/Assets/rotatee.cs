using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatee : MonoBehaviour
{
    float yRot;

    // Update is called once per frame
    void FixedUpdate()
    {
        yRot++;
        transform.rotation = Quaternion.Euler(yRot * Vector3.forward);
    }
}
