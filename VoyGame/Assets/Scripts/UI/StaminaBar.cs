using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Stamina _staminaScript;
    
    private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _staminaScript.maxStamina;
    }

    public void SetStaminaValue(float valueNow) => _slider.value = valueNow;
}
