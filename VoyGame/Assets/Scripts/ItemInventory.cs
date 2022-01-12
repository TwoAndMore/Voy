using System.Collections;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private const KeyCode TAKEPILLS = KeyCode.G;

    [SerializeField] private AdrenalineText _adrenalineText;
    
    private Stamina _staminaScript;
    private float _durability = 20f;
    private float _increaseModAdder = 10f;
    private float _decreaseRest = 1.5f;

    public int maxPillsAmount = 3;
    public int pillsAmount;
    
    private void Awake() => _staminaScript = GetComponent<Stamina>();

    private void Update()
    {
        TakePills();
    }
    
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
}
