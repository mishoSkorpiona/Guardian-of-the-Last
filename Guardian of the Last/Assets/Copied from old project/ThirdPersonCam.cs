using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float sensitivity = 1f;
    public float offset = 0.1f;

    public Camera cam;
    public LayerMask cameraCantGoThrough;

    public Transform Ybody;
    public Transform Xbody;
    float xRotation;
    float yRotation;

    //fuck this bullshit
    GuardianoftheLast input;
    private void Awake()
    {
        input = new GuardianoftheLast();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        Vector2 deltaMouse = input.Player.Look.ReadValue<Vector2>();

        deltaMouse *= sensitivity / 50f;

        xRotation -= deltaMouse.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += deltaMouse.x;

        yRotation %= 360;

        Ybody.localRotation = Quaternion.Euler(0, yRotation, 0);
        Xbody.localRotation = Quaternion.Euler(xRotation, 0, 0);

        if (Physics.Raycast(new Ray(Xbody.position, -transform.forward), out RaycastHit rch, 8, cameraCantGoThrough))
        {
            transform.position = rch.point;
            transform.position += transform.forward * offset;
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, -8);
        }


        //transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //body.Rotate(Vector3.up * deltaMouse.x);
    }
    
    public void SetCameraActive(bool active)
    {
        cam.enabled = active;
    }
}
