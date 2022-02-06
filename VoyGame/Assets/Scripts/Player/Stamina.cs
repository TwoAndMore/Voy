using System;
using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private StaminaBar _staminaBarScript;
    
    private PlayerMovement _playerMovementScript;
    private float _decreaseMod = 25f;
    private float _currentStamina = 100f;

    [HideInInspector] public bool isLow;

    public float maxStamina = 100f;
    public float increaseMod = 15f;
    public float timeRest = 3f;

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
                _currentStamina += Time.deltaTime * increaseMod;
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

    public void TakePills(float increaseModAdder,float decreaseRest, float durability)
    {
        StopCoroutine(PillsTaken(increaseModAdder, decreaseRest, durability));
        StartCoroutine(PillsTaken(increaseModAdder, decreaseRest, durability));
    }

    IEnumerator Rest()
    {
        isLow = true;
        yield return new WaitForSeconds(timeRest);
        isLow = false;
    }

    private IEnumerator PillsTaken(float increaseModAdder, float decreaseRest, float durability)
    {
        Debug.Log("Started");
        increaseMod += increaseModAdder;
        timeRest -= decreaseRest;
        yield return new WaitForSeconds(durability);
        Debug.Log("Ended");
        increaseMod -= increaseModAdder;
        timeRest += decreaseRest;
    }
}
