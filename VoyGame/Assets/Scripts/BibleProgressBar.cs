using UnityEngine;
using UnityEngine.UI;

public class BibleProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    
    private Coffin _coffinScript;
    private Slider _slider;

    private void Awake()
    {
        _coffinScript = GameObject.Find("Coffin").GetComponent<Coffin>();
        _slider = GetComponent<Slider>();
        _slider.maxValue = _coffinScript.maxBibleProgressValue;
        _slider.value = _coffinScript.currentBibleProgressValue;
    }
    
    private void Update()
    {
        _slider.value = _coffinScript.currentBibleProgressValue;
        _fillImage.fillAmount = _slider.value / _slider.maxValue;
    }
}
