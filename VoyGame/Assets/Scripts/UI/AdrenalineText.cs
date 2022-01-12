using TMPro;
using UnityEngine;

public class AdrenalineText : MonoBehaviour
{
    [SerializeField] private ItemInventory _itemInventoryScript;
    
    private TextMeshProUGUI _text;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    public void SetText() => _text.text = _itemInventoryScript.pillsAmount.ToString();
}
