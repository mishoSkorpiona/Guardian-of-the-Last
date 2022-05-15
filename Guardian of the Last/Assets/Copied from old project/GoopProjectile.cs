using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopProjectile : MonoBehaviour
{
    public GameObject myCamera;
    public Rigidbody myRigidbody;
    public LayerMask stickMask;

    public ThirdPersonCam playerCamera;

    //this is public so the thrower can know when it has stuck
    public bool stuck;

    public float throwDistance = 100f;
    Vector3 throwDirection;

    private void Start()
    {
        throwDirection = transform.forward + Mathf.Clamp01(-transform.forward.y) * 3 * Vector3.up;

        myRigidbody.AddForce(throwDirection * throwDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // now this is a little maneuvre i got from stackoverflow
        // it's a nice little bit hack to see if the layer it has collided with is in the stickmask
        if (stickMask == (stickMask | (1 << collision.gameObject.layer)))
        {
            //we've hit a floor
            stuck = true;

            Destroy(myRigidbody);

            //this bit makes the projectile camera the main camera
            myCamera.SetActive(true);
            playerCamera.SetCameraActive(false);

            playerCamera.enabled = false;

            //The gimble gets fucked if we don't reset the rotation
            transform.rotation = Quaternion.identity;

            //if you think it's easy, code it so the camera doesn't always spawn facing north, but the direction it was thrown here:
            throwDirection.y = 0;
            
        }
    }

    //private void OnDrawGizmos()
    //{
    //    // this draws the collider in the scene, remove this when working with the actual model
    //    Gizmos.color = new Color(0.69f, /*nice*/ 0.043f, 0.898f); // this is colour #B00BE5
    //    Gizmos.DrawSphere(transform.position, 0.5f);
    //}
}
