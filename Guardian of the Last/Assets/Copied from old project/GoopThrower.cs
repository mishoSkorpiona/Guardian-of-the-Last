using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoopThrower : MonoBehaviour
{
    //spawnOffset is the distance between the projectile spawn position and our position, should be tweaked
    public float spawnOffset = 0.5f;
    public GameObject projectilePrefab;
    public Transform spawnPosition;
    public Animator anim;
    public SAudioManager am;

    GameObject spawnedProjectile;
    GoopProjectile goopProjectile;

    ThirdPersonCam playerCam;

    GuardianoftheLast input;
    private void Awake()
    {
        input = new GuardianoftheLast();

        input.Player.Fire.performed += PrimaryFire;
        input.Player.AltFire.performed += SecondaryFire;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        playerCam = GetComponentInChildren<ThirdPersonCam>();
    }

    public void PrimaryFire(InputAction.CallbackContext c)
    {
        if (goopProjectile == null)
        {
            StartCoroutine(ThrowProjectile());
        }
        else if (goopProjectile.stuck)
        {
            Teleport();
        }
        else
        {
            KillProjectile();
        }
    }

    public void SecondaryFire(InputAction.CallbackContext c)
    {
        KillProjectile();
    }

    public void KillProjectile()
    {
        am.Play("ProjectileDeath");

        Destroy(spawnedProjectile);

        playerCam.enabled = true;
        playerCam.SetCameraActive(true);

        goopProjectile = null;
    }

    public void Teleport()
    {
        am.Play("Teleport");

        Transform projectileTransform = goopProjectile.transform;

        transform.SetPositionAndRotation(projectileTransform.position, projectileTransform.rotation);
        KillProjectile();
    }

    public IEnumerator ThrowProjectile()
    {
        am.Play("Fling");

        anim.SetTrigger("Throw");

        yield return new WaitForSeconds(0.5f);

        Transform cameraTransform = playerCam.transform;

        spawnedProjectile = Instantiate(projectilePrefab, spawnPosition.position + cameraTransform.forward * spawnOffset, cameraTransform.rotation);

        goopProjectile = spawnedProjectile.GetComponent<GoopProjectile>();
        goopProjectile.playerCamera = playerCam;
    }
}
