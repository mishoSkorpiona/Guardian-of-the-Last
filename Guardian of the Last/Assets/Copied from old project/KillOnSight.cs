using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillOnSight : MonoBehaviour
{
    public LayerMask playerMask;
    public LayerMask wallMask;

    private void OnTriggerEnter(Collider collision)
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
                go.GetComponentInChildren<ThirdPersonCam>().enabled = false;
            }
        }
    }
}
