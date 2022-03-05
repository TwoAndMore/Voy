using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private Vector3 _startCameraPosition;
    private float _crouchOffset = -0.2f;

    private void Start() => 
        _startCameraPosition = transform.localPosition;

    private void Update() => 
        transform.localPosition = _playerMovement.isCrouching ? new Vector3(_startCameraPosition.x, _startCameraPosition.y + _crouchOffset, _startCameraPosition.z) : _startCameraPosition;
}
