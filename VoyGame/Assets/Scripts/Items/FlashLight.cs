using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject _lightSource;

    private AudioSource _clickSound;
    private bool _isOn;
    private void Awake() => _clickSound = GetComponent<AudioSource>();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isOn = !_isOn;
            _lightSource.SetActive(_isOn);
            _clickSound.Play();
        }
    }
}
