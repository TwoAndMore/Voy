using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Stamina _staminaScript;
    [SerializeField] private Image _fillImage;
    
    private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _staminaScript.maxStamina;
    }

    public void SetStaminaValue(float valueNow)
    {
        _slider.value = valueNow;
        _fillImage.fillAmount = _slider.value / _slider.maxValue;
    }
}
