using UnityEngine;
using UnityEngine.UI;

public class BibleProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    
    private CoffinEvent _coffinScript;
    private Slider _slider;

    private void Awake()
    {
        _coffinScript = GameObject.Find("Coffin").GetComponent<CoffinEvent>();
        _slider = GetComponent<Slider>();
        _slider.maxValue = _coffinScript.maxBibleProgress;
        _slider.value = _coffinScript.currentBibleProgress;
    }
    
    private void Update()
    {
        _slider.value = _coffinScript.currentBibleProgress;
        _fillImage.fillAmount = _slider.value / _slider.maxValue;
    }
}
