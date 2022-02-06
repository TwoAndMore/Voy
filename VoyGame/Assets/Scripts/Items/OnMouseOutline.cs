using UnityEngine;

public class OnMouseOutline : MonoBehaviour
{
    private Outline _outlineScript;
    
    private void Awake() => 
        _outlineScript = GetComponent<Outline>();

    private void OnMouseEnter() => 
        _outlineScript.enabled = true;

    private void OnMouseExit() => 
        _outlineScript.enabled = false;
}
