using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float STANDARTSPEED = 10f;
    private const float RUNSPEED = 150f;
    private const float CROUCHSPEED = 5f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _speed = STANDARTSPEED;
    private float _gravity = -9.81f;
    private float _jumpHeight = 3f;
    private float _groundDistance = 0.4f;
    private bool _isGrounded;
    private bool _isCrouch;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        //Running
        if (Input.GetKey(KeyCode.LeftShift) && !_isCrouch && _isGrounded)
            _speed = RUNSPEED;
        else
            _speed = STANDARTSPEED;

        //Crouching
        if (Input.GetKey(KeyCode.LeftControl) && _isGrounded)
        {
            _isCrouch = true;
            _speed = CROUCHSPEED;
        }
        else
        {
            _isCrouch = false;
            _speed = STANDARTSPEED;
        }
        
        Jump();
    }

    private void Jump()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        
        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); 
        }
        
        if(_isGrounded &&  _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }
}
