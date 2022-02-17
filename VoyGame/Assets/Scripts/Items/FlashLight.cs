using UnityEngine;
using Photon.Pun;

public class FlashLight : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _lightSource;

    private AudioSource _audioSource;
    private bool _isOn;
    
    private void Awake() => 
        _audioSource = GetComponent<AudioSource>();
    
    private void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            _isOn = !_isOn;
            _lightSource.SetActive(_isOn);
            _audioSource.Play();
        }
    }
}
