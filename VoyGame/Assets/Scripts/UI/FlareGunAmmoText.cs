using TMPro;
using UnityEngine;

public class FlareGunAmmoText : MonoBehaviour
{
    [SerializeField] private ItemInventory _itemInventory;

    private TextMeshProUGUI _text;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    public void SetText() => _text.text = _itemInventory.currentAmmoAmount.ToString();
}
