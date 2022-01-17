using System.Collections;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private const KeyCode TAKEPILLS = KeyCode.G;
    
    [SerializeField] private Transform _itemsPosition;
    
    #region PillsVariables
    [SerializeField] private AdrenalineText _adrenalineText;
    
    private Stamina _staminaScript;
    private float _durability = 20f;
    private float _increaseModAdder = 10f;
    private float _decreaseRest = 1.5f;

    public int maxPillsAmount = 3;
    public int pillsAmount;
    #endregion
    
    #region FlageGunVariables
    //Ammo
    [SerializeField] private FlareGunAmmoText _flareGunAmmoText;
    
    public int maxAmmoAmount;
    public int currentAmmoAmount;
    
    //Gun
    [SerializeField] private GameObject _gunPrefab;
    [SerializeField] private FlareGunAmmoText _flareGunAmmoTextScript;
    #endregion

    #region LampVariables
    [SerializeField] private GameObject _lampPrefab;
    #endregion
    
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
        GameObject gun = Instantiate(_gunPrefab, _itemsPosition);
        gun.GetComponent<FlareGunShoot>().itemInventoryScript = this;
        gun.GetComponent<FlareGunShoot>().flareGunAmmoTextScript = _flareGunAmmoTextScript;
        gun.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    #endregion

    #region HandLamp

    public void AddLamp()
    {
        GameObject lamp = Instantiate(_lampPrefab, _itemsPosition);
        lamp.transform.localPosition = new Vector3(0f, 0.03f, 0.15f);
    } 

    #endregion
}
