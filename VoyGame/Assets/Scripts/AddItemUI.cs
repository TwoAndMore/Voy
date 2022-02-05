using UnityEngine;

public class AddItemUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;

    public void AddItem(int itemID) => _items[itemID].SetActive(true);
}
