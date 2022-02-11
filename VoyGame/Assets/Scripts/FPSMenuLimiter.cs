using UnityEngine;

public class FPSMenuLimiter : MonoBehaviour
{
    private void Start() => 
        Application.targetFrameRate = 60;
}
