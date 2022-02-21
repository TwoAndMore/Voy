using Photon.Pun;
using UnityEngine;

public class FireTorch : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _fireLight;
    [SerializeField] private AudioClip _activate;
    
    private QuestItemsInventory _questItemsInventoryScript;
    private Outline _outline;
    private OnMouseOutline _onMouseOutline;
    private AudioSource _audioSource;
    private bool _isActive;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _outline = GetComponent<Outline>();
        _onMouseOutline = GetComponent<OnMouseOutline>();
        _questItemsInventoryScript = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
    }

    private void OnMouseDown()
    {
        if(!_questItemsInventoryScript.questItemsArray[6].isFinded || _isActive)
            return;
        
        photonView.RPC("ActivateTorch", RpcTarget.AllViaServer);
    }

    [PunRPC]
    private void ActivateTorch()
    {
        _isActive = true;
        _fireLight.SetActive(true);
        _audioSource.PlayOneShot(_activate);
        Destroy(_onMouseOutline);
        Destroy(_outline);
    }
}
