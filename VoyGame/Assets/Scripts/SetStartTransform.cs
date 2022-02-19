using UnityEngine;

public class SetStartTransform : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Quaternion _rotation;
    [SerializeField] private Vector3 _scale;

    private Transform _transform;
    
    private void Awake() => 
        _transform = transform;

    private void Start()
    {
        _transform.localPosition = _position;
        _transform.localRotation = _rotation;
        _transform.localScale = _scale;
    }
}
