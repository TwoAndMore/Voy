using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class ItemInventory : MonoBehaviourPunCallbacks
{
    private const KeyCode TAKEPILLS = KeyCode.G;
    private const string HANDITEMSPATH = "Prefabs/Hand Items/";
    
    [SerializeField] private Transform _itemsPosition;

    public bool haveMainItem;

    [Header("Pills")] [SerializeField] private AdrenalineText _adrenalineText;

    private Stamina _staminaScript;
    private float _durability = 20f;
    private float _increaseModAdder = 10f;
    private float _decreaseRest = 1.5f;

    public int maxPillsAmount = 3;
    public int pillsAmount;

    [Header("FlareGun")] 
    [SerializeField] private FlareGunAmmoText _flareGunAmmoText;
    [SerializeField] private GameObject _flareGunAmmoUI;

    public bool haveGun;
    public int maxAmmoAmount;
    public int currentAmmoAmount;

    [SerializeField] private GameObject _gunPrefab;
    [SerializeField] private FlareGunAmmoText _flareGunAmmoTextScript;

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
        _flareGunAmmoText.SetText();
    }

    public void AddFlareGun()
    {
        haveMainItem = true;
        haveGun = true;
        _flareGunAmmoUI.SetActive(true);
        GameObject gun = Instantiate(_gunPrefab, _itemsPosition);
        gun.GetComponent<FlareGunShoot>().itemInventoryScript = this;
        gun.GetComponent<FlareGunShoot>().flareGunAmmoTextScript = _flareGunAmmoTextScript;
        gun.transform.localPosition = new Vector3(0f, 0f, 0f);
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
        GameObject compass = Instantiate(_compassPrefab, _itemsPosition);
        compass.transform.localPosition = new Vector3(0.15f, 0.085f, 0.14f);
        compass.transform.localRotation = Quaternion.Euler(-3f, -30f, 30f);
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
        GameObject mainItem = null, mainPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PhotonView[] thingsView = FindObjectsOfType<PhotonView>();
        
        foreach (PhotonView thing in thingsView)
        {
            if (thing.ViewID == thingID)
                mainItem = thing.gameObject;
        }
        
        if(mainItem == null)
                return;
        
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().OwnerActorNr == mainItem.GetComponent<PhotonView>().OwnerActorNr)
                mainPlayer = player;
        }

        if(mainPlayer != null)
            mainItem.transform.parent = mainPlayer.transform.Find("CameraHolder/SwayThings");
    }
}
