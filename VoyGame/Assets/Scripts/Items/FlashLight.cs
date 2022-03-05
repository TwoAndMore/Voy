using UnityEngine;
using Photon.Pun;

public class FlashLight : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _bodyCamera;
    [SerializeField] private GameObject _lightSource;
    [SerializeField] private PhotonView _photonView;
    
    private AudioSource _audioSource;
    private bool _isOn;
    
    private void Awake() => 
        _audioSource = GetComponent<AudioSource>();
    
    private void Update()
    {
        if(!_photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        transform.localRotation = Quaternion.Euler(new Vector3(0f, -98, -(_bodyCamera.eulerAngles.x - 5)));
        
        if (Input.GetKeyDown(KeyCode.T)) 
            _photonView.RPC("FlashLightActivation", RpcTarget.All);
    }

    [PunRPC]
    private void FlashLightActivation()
    {
        _isOn = !_isOn;
        _lightSource.SetActive(_isOn);
        _audioSource.Play();
    }
}
