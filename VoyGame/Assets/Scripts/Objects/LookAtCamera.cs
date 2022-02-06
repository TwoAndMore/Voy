using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject _camera;

    private void Awake() =>
        _camera = GameObject.FindWithTag("MainCamera");

    private void Update() => 
        gameObject.transform.LookAt(_camera.transform);
}
