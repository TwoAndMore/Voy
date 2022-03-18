using System;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public void Debug1er(float kol)
    {
        Debug.Log("INT: " + kol);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _slider.value += 10;
    }
}
