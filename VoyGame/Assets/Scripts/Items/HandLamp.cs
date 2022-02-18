using UnityEngine;
using Photon.Pun;

public class HandLamp : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _lightSource;
    [SerializeField] private AudioClip _clickSound;

    private Renderer _renderer;
    private AudioSource _audioSource;
    private bool _isOn;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
        if(Input.GetKeyDown(KeyCode.E))
            photonView.RPC("HandLampActivation", RpcTarget.All);
    }
    
    [PunRPC]
    private void HandLampActivation()
    {
        _isOn = !_isOn;
        _lightSource.SetActive(_isOn);
        _audioSource.PlayOneShot(_clickSound);

        _renderer.material.SetColor("_EmissionColor", _isOn ? new Color(255f, 255f, 0f) : Color.gray);
    }
}
