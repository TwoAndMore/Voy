using System;
using UnityEngine;

public class ObjectOutline : MonoBehaviour
{
    private Outline _outline;
    
    private void Awake() => _outline = GetComponent<Outline>();

    private void OnTriggerEnter(Collider other) => _outline.enabled = true;
    private void OnTriggerExit(Collider other) => _outline.enabled = false;
}
