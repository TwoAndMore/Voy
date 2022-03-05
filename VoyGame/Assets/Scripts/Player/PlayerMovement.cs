using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    private const float STANDARTSPEED = 7f;
    private const float RUNSPEED = 10f;
    private const float CROUCHSPEED = 5f;

    [SerializeField] private GameObject _crouchPicture;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Animator _animator;

    private Stamina _staminaScript;
    private CharacterController _controller;
    private Vector3 _velocity;
    private float _speed = STANDARTSPEED;
    private float _gravity = -30f;
    private float _jumpHeight = 1f;
    private float _groundDistance = 0.4f;
    private bool _isGrounded;

    public bool isCrouching;
    public bool isRunning;

    private void Awake()
    {
        _staminaScript = GetComponent<Stamina>();
        _controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
        if(PauseMenu.isPaused)
            return;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        //Running
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && _isGrounded && _staminaScript.HaveStamina() && !_staminaScript.isLow)
        {
            isRunning = true;
            _speed = RUNSPEED;
        }
        else
        {
            _speed = STANDARTSPEED;
            isRunning = false;
        }

        //Crouching
        if (Input.GetKey(KeyCode.LeftControl) && _isGrounded)
        {
            isCrouching = true;
            _speed = CROUCHSPEED;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            _speed = STANDARTSPEED;
        }
        
        _crouchPicture.SetActive(isCrouching);
        
        Jump();

        //Animations
        _animator.SetBool("isWalking", move.x+move.z != 0f);
        _animator.SetBool("isRunning", _speed == RUNSPEED && move.x+move.z != 0f);
        _animator.SetBool("isCrouching", isCrouching);
        _animator.SetBool("isCrouchWalking", isCrouching && move.x+move.z != 0f);
        _animator.SetBool("isJumping", !_isGrounded);
    }

    private void Jump()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        
        if(Input.GetButtonDown("Jump") && _isGrounded && !isCrouching)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _animator.SetTrigger("Jump");
        }
        
        if(_isGrounded &&  _velocity.y < 0) 
            _velocity.y = -2f;
    }
}
