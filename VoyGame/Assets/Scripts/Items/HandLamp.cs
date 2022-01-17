using UnityEngine;

public class HandLamp : MonoBehaviour
{
    [SerializeField] private GameObject _lightSource;
    [SerializeField] private AudioClip _clickSound;
    
    private bool _isOn;

    private void Update()
    {
        HandLampActivation();
    }

    private void HandLampActivation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isOn = !_isOn;
            _lightSource.SetActive(_isOn);
            GetComponent<AudioSource>().PlayOneShot(_clickSound);

            if (_isOn)
                GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255f, 255f, 0f));
            else
                GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.gray);
        }
    }
}
