using UnityEngine;

public class FPSMenuLimiter : MonoBehaviour
{
    [SerializeField] private int _frameLimit = 60;
    
    private void Start() => 
        Application.targetFrameRate = _frameLimit;
}
