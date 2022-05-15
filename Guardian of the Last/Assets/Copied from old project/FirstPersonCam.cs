using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public Camera cam;

    public float sensitivity;
    private float xRotation;

    public Transform body;

    //fuck this bullshit
    GuardianoftheLast input;
    private void Awake()
    {
        input = new GuardianoftheLast();
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

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        body.Rotate(Vector3.up * deltaMouse.x);
    }

    public void SetCameraActive(bool active)
    {
        cam.enabled = active;
    }
}
