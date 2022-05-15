using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerMisho : MonoBehaviour
{
    public Transform localDirection;

    Vector2 input;

    private Rigidbody _rb;

    [SerializeField] private float _speed = 2;

    [SerializeField] private GameObject _baseBone;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = input.y * localDirection.forward + input.x * localDirection.right;

        movement.Normalize();

        movement *= _speed * Time.deltaTime;
        movement.y = 0;

        _rb.AddForce(movement * 500);
        dragBottom();
    }

    void OnMove (InputValue inputVal)
    {
        input.x = inputVal.Get<Vector2>().x;
        input.y = inputVal.Get<Vector2>().y;
    }

    void dragBottom()
    {
        _baseBone.transform.localPosition = -_rb.velocity.normalized * 0.01f;
    }

}