using Photon.Pun;
using UnityEngine;

public class Totems : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _activeEffects;
    [SerializeField] private AudioClip _activate;
    
    private QuestItemsInventory _questItemsInventoryScript;
    private Outline _outline;
    private OnMouseOutline _onMouseOutline;
    private AudioSource _audioSource;
    private bool _isActive;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _onMouseOutline = GetComponent<OnMouseOutline>();
        _outline = GetComponent<Outline>();
        _questItemsInventoryScript = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
    }

    private void OnMouseDown()
    {
        if(_isActive)
            return;
        
        photonView.RPC("ActivateTotem", RpcTarget.AllViaServer);
    }

    [PunRPC]
    private void ActivateTotem()
    {
        if (!_questItemsInventoryScript.HaveElementalItems()) 
            return;
        
        _isActive = true;
        _activeEffects.SetActive(true);
        _audioSource.PlayOneShot(_activate);
        Destroy(_onMouseOutline);
        Destroy(_outline);
    }
}
