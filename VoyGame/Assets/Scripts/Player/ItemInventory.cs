using System.Collections;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private const KeyCode TAKEPILLS = KeyCode.G;

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

    private void Awake() => _staminaScript = GetComponent<Stamina>();

    private void Update()
    {
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
        GameObject lamp = Instantiate(_lampPrefab, _itemsPosition);
        lamp.transform.localPosition = new Vector3(0f, 0.03f, 0.15f);
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
}
