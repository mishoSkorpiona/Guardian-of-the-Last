using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Camera cam;

    GuardianoftheLast input;

    private void Awake()
    {
        input = new GuardianoftheLast();
    }

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        
    }

    public void SetCameraActive(bool active)
    {
        cam.enabled = active;
    }
}
