using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillOnSight : MonoBehaviour
{
    public Animator a;
    public CycleFollower cf;

    public LayerMask playerMask;
    public LayerMask wallMask;

    private void OnTriggerStay(Collider collision)
    {
        GameObject go = collision.gameObject;

        // it's a nice little bit hack to see if the layer it has collided with is in the stickmask
        if (playerMask == (playerMask | (1 << go.layer)))
        {
            //we know it's the player, now check to see if we're seeing them through walls

            Vector3 directionTooPlayer = collision.transform.position - transform.position;
            float distanceToPlayer = directionTooPlayer.magnitude;
            directionTooPlayer.Normalize();

            if (!Physics.Raycast(new Ray(transform.position, directionTooPlayer), distanceToPlayer, wallMask))
            {
                Debug.Log("I've found the player!", gameObject);

                //this will need to get bigger as we give more shit to the player.  Please add in moderation
                //also yes this isn't readable no matter how many empty line I add.  Fuck me ¯\_:]_/¯
                go.GetComponent<PlayerInput>().enabled = false;
                go.GetComponent<GoopThrower>().KillProjectile();
                go.GetComponent<GoopThrower>().enabled = false;
                ThirdPersonCam playerCam = go.GetComponentInChildren<ThirdPersonCam>();
                playerCam.enabled = false;

                playerCam.cam.transform.rotation = Quaternion.LookRotation((transform.position + Vector3.up * 1.7f) - playerCam.cam.transform.position, Vector3.up);
                playerCam.cam.transform.parent = transform;

                //Jeff wants me to fuck with camera?  I fuck with camera
                StartCoroutine(FuckWithCamera(playerCam.cam));

                a.SetTrigger("Game Over");
                Destroy(cf);
            }
        }
    }

    public IEnumerator FuckWithCamera(Camera c)
    {
        float minFOV = 5;
        float timeInSeconds = 0;

        while (true)
        {
            timeInSeconds += Time.fixedDeltaTime;
            c.fieldOfView = Mathf.Lerp(90, minFOV, Mathf.Min(1, timeInSeconds / 0.7f));
            yield return new WaitForFixedUpdate();
        }
    }
}
