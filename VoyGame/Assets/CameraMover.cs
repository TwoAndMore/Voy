using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    
    [Range(0, -1)][SerializeField] private float _tir;

    private Vector3 _startCameraPosition;
    private void Start()
    {
        _startCameraPosition = transform.localPosition;
    }

    private void Update()
    {
        if (_playerMovement.isCrouching)
            transform.localPosition = new Vector3(_startCameraPosition.x, _startCameraPosition.y - _tir, _startCameraPosition.z);
        else
            transform.localPosition = _startCameraPosition;
    }
}
