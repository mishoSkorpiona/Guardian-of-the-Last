using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerMisho : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;

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
        _rb.AddForce(moveDirection * _speed * Time.deltaTime*500);
        dragBottom();
    }

    void OnMove (InputValue input)
    {
        moveDirection.x = input.Get<Vector2>().x;
        moveDirection.z = input.Get<Vector2>().y;
    }

    void dragBottom()
    {
         
        _baseBone.transform.localPosition = -_rb.velocity.normalized * 0.01f;
    }

}