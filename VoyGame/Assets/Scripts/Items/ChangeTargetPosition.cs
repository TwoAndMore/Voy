using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ChangeTargetPosition : MonoBehaviour
{
    [SerializeField] private Rig _rig;
    
    private Transform _transform;

    private void Awake() => 
        _transform = GetComponent<Transform>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            ActivateTarget("Compass");
    }
    
    public void ActivateTarget(string itemName)
    {
        _rig.weight = 1f;
        _transform.localPosition = GetTargetTransform.GetTransform(itemName);
        _transform.localRotation = Quaternion.Euler(GetTargetTransform.GetRotation(itemName));
    }
}
