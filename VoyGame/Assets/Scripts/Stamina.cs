using System;
using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private StaminaBar _staminaBarScript;
    
    private PlayerMovement _playerMovementScript;
    private float _increaseMod = 15f;
    private float _decreaseMod = 25f;
    private float _timeRest = 3f;
    private float _currentStamina = 100f;

    [HideInInspector] public bool isLow;

    public float maxStamina = 100f;

    private void Awake()
    {
        _playerMovementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_playerMovementScript.isRunning)
            _currentStamina -= Time.deltaTime * _decreaseMod;
        else
        {
            if (_currentStamina <= maxStamina) 
                _currentStamina += Time.deltaTime * _increaseMod;
        }

        if (_currentStamina <= 1)
        {
            if (isLow)
                return;
            StopCoroutine(Rest());
            StartCoroutine(Rest());
        }
        _staminaBarScript.SetStaminaValue(_currentStamina);
    }
    public bool HaveStamina() => _currentStamina > 0;

    IEnumerator Rest()
    {
        isLow = true;
        yield return new WaitForSeconds(_timeRest);
        isLow = false;
    }
}
