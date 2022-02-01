using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    [Range(0,50)] [SerializeField] private float _speedScale;
    private void Awake() => _speedScale = Time.timeScale;
    private void Update() => Time.timeScale = _speedScale;
}
