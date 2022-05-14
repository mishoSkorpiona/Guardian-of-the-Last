using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float sensitivity = 1f;

    public Camera cam;

    public Transform body;
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

        body.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

        //transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //body.Rotate(Vector3.up * deltaMouse.x);
    }
    
    public void SetCameraActive(bool active)
    {
        cam.enabled = active;
    }
}
