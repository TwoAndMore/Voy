using UnityEngine;
using UnityEngine.UI;

public class BibleProgressBar : MonoBehaviour
{
    private Coffin _coffinScript;
    private Slider _slider;

    private void Awake()
    {
        _coffinScript = GameObject.Find("Coffin").GetComponent<Coffin>();
        _slider = GetComponent<Slider>();
        _slider.maxValue = _coffinScript.maxBibleProgressValue;
        _slider.value = _coffinScript.currentBibleProgressValue;
    }
    
    private void Update() => 
        _slider.value = _coffinScript.currentBibleProgressValue;
}
