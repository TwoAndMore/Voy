using System.Collections;
using Photon.Pun;
using UnityEngine;

public class ItemInventory : MonoBehaviourPunCallbacks
{
    private const KeyCode TAKEPILLS = KeyCode.G;
    private const string HANDITEMSPATH = "Prefabs/Hand Items/";
    
    [SerializeField] private Transform _itemsPosition;

    [SerializeField] private GameObject _mainItem;
    [SerializeField] private GameObject _mainPlayer;
    
    public bool haveMainItem;

    [Header("Pills")] [SerializeField] private AdrenalineText _adrenalineText;

    private Stamina _staminaScript;
    private float _durability = 20f;
    private float _increaseModAdder = 10f;
    private float _decreaseRest = 1.5f;

    public int maxPillsAmount = 3;
    public int pillsAmount;

    [Header("FlareGun")] 
    [SerializeField] private GameObject _flareGunAmmoUI;
    [SerializeField] private GameObject _gunPrefab;
    
    public FlareGunAmmoText flareGunAmmoTextScript;
    public bool haveGun;
    public int maxAmmoAmount;
    public int currentAmmoAmount;


    [Header("Lamp")] 
    [SerializeField] private GameObject _lampPrefab;

    [Header("Compass")] 
    [SerializeField] private GameObject _compassPrefab;
    [SerializeField] private GameObject _compassUI;

    [Header("Mirror")] 
    [SerializeField] private GameObject _mirrorPrefab;

    public int _code;

    private void Awake() => _staminaScript = GetComponent<Stamina>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            photonView.TransferOwnership(_code);
        
        TakePills();
    }

    #region Pills

    private void TakePills()
    {
        if (Input.GetKeyDown(TAKEPILLS))
        {
            if (pillsAmount <= 0)
            {
                Debug.Log("TEXT + SOUND");
            }
            else
            {
                pillsAmount -= 1;
                _adrenalineText.SetText();
                StopCoroutine(PillsTaken());
                StartCoroutine(PillsTaken());
            }
        }
    }

    public void AddPill()
    {
        pillsAmount += 1;
        _adrenalineText.SetText();
    }

    private IEnumerator PillsTaken()
    {
        _staminaScript.increaseMod += _increaseModAdder;
        _staminaScript.timeRest -= _decreaseRest;
        yield return new WaitForSeconds(_durability);
        _staminaScript.increaseMod -= _increaseModAdder;
        _staminaScript.timeRest += _decreaseRest;
    }
    
    #endregion

    #region FlareGun

    public void AddFlareGunAmmo()
    {
        currentAmmoAmount += 1;
        
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
        flareGunAmmoTextScript.SetText();
    }

    public void AddFlareGun()
    {
        haveMainItem = true;
        haveGun = true;
        _flareGunAmmoUI.SetActive(true);
        if (photonView.IsMine)
        {
            GameObject gun = PhotonNetwork.Instantiate(HANDITEMSPATH + _gunPrefab.name, _itemsPosition.position, Quaternion.identity);
            photonView.RPC("SetObjectParent", RpcTarget.All, gun.GetComponent<PhotonView>().ViewID);
            photonView.RPC("SetGunScripts", RpcTarget.All);
        }
    }

    [PunRPC]
    private void SetGunScripts()
    {
        FlareGunShoot gunShootScript = _mainItem.GetComponent<FlareGunShoot>();
        gunShootScript.itemInventoryScript = this;
        gunShootScript.flareGunAmmoTextScript = flareGunAmmoTextScript;
    }  
    #endregion

    #region HandLamp
    
    public void AddLamp()
    {
        haveMainItem = true;

        if (photonView.IsMine)
        {
            GameObject lamp = PhotonNetwork.Instantiate(HANDITEMSPATH + _lampPrefab.name, _itemsPosition.position, Quaternion.identity);
            photonView.RPC("SetObjectParent", RpcTarget.All, lamp.GetComponent<PhotonView>().ViewID);
        }
    }
    
    #endregion

    #region Compass

    public void AddCompass()
    {
        haveMainItem = true;

        Instantiate(_compassPrefab, _itemsPosition);
        _compassUI.SetActive(true);
    }

    #endregion

    #region Mirror

    public void AddMirror()
    {
        haveMainItem = true;
        GameObject mirror = Instantiate(_mirrorPrefab, _itemsPosition);
        mirror.transform.localPosition = new Vector3(0f, 0.13f, 0f);
        mirror.GetComponent<Mirror>().player = gameObject;
    }
    
    #endregion
    
    [PunRPC]
    private void SetObjectParent(int thingID)
    {
        _mainItem = _mainPlayer = null;
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PhotonView[] thingsView = FindObjectsOfType<PhotonView>();
        
        foreach (PhotonView thing in thingsView)
        {
            if (thing.ViewID == thingID)
                _mainItem = thing.gameObject;
        }
        
        if(_mainItem == null)
                return;
        
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().OwnerActorNr == _mainItem.GetComponent<PhotonView>().OwnerActorNr)
                _mainPlayer = player;
        }

        if(_mainPlayer != null)
            _mainItem.transform.parent = _mainPlayer.transform.Find("CameraHolder/SwayThings");
    }
}
