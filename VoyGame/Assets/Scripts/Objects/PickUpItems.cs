using UnityEngine;
using Photon.Pun;

public class PickUpItems : MonoBehaviourPunCallbacks
{
    private const KeyCode PICKUP = KeyCode.E;

    [SerializeField] private AudioClip _pickUpSound;

    private ItemInventory _itemInventoryScript;
    private AudioSource _audioSource;
    private GameObject _item;
    private string[] _itemTags = {"Adrenaline", "FlareGun", "FlareAmmo", "HandLamp", "Compass", "Mirror"};
    private bool _inRange;
    private int _triggerEnters;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _itemInventoryScript = GetComponent<ItemInventory>();
    }

    private void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
        if (Input.GetKeyDown(PICKUP) && _inRange && _item != null) 
            photonView.RPC("TakeItem",  RpcTarget.AllViaServer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cross"))
            return;
        
        _inRange = true;
        _triggerEnters++;
        _item = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        _triggerEnters--;
        if (_triggerEnters <= 0)
        {
            _triggerEnters = 0;
            _item = null;
            _inRange = false;
        }
    }
    
    [PunRPC]
    private void TakeItem()
    {
        if (!ItIsObject())
            return;

        if (_item.CompareTag(_itemTags[0]))
        {
            if(_itemInventoryScript.pillsAmount < _itemInventoryScript.maxPillsAmount)
                _itemInventoryScript.AddPill();
            else return;
        }
        else if (_item.CompareTag(_itemTags[1]))
        {
            if(_itemInventoryScript.haveMainItem)
                return;
            _itemInventoryScript.AddFlareGun();
        }
        else if (_item.CompareTag(_itemTags[2]))
        {
            if (_itemInventoryScript.currentAmmoAmount < _itemInventoryScript.maxAmmoAmount && _itemInventoryScript.haveGun)
                _itemInventoryScript.AddFlareGunAmmo();
            else return;
        }
        else if (_item.CompareTag(_itemTags[3]))
        {
            if(_itemInventoryScript.haveMainItem)
                return;

            _itemInventoryScript.AddLamp();
            //photonView.RPC("AddLamp", RpcTarget.All);
        }
        else if (_item.CompareTag(_itemTags[4]))
        {
            if(_itemInventoryScript.haveMainItem)
                return;
            _itemInventoryScript.AddCompass(); 
        }
        else if (_item.CompareTag(_itemTags[5]))
        {
            if(_itemInventoryScript.haveMainItem)
                return;
            _itemInventoryScript.AddMirror(); 
        }
        
        _audioSource.PlayOneShot(_pickUpSound);
        _triggerEnters--;
        Destroy(_item);
    }

    private bool ItIsObject()
    {
        bool tagItem = false;
        foreach (string tag in _itemTags)
        {
            if (_item.CompareTag(tag))
                tagItem = true;
        }

        return tagItem;
    }
}
