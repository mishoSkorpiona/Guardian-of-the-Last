using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public Camera cam;

    [SerializeField]
    private float sensetivity;
    private float xRotation;
    [SerializeField]
    private Transform body;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        body.Rotate(Vector3.up * mouseX);
    }

    public void SetCameraActive(bool active)
    {
        cam.enabled = active;
    }
}
