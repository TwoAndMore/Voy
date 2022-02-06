using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] [Range(0,6)] private int _itemID;

    private QuestItemsInventory _questItemsInventoryScript;
    private Outline _outlineScript;
    private void Awake()
    {
        _outlineScript = GetComponent<Outline>();
        _questItemsInventoryScript = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
    }
        
    
    private void OnMouseEnter() => _outlineScript.enabled = true;

    private void OnMouseExit() => _outlineScript.enabled = false;

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        bool firstClick = false;
        if(firstClick)
            return;
        firstClick = true;
        
        GetComponent<AudioSource>().PlayOneShot(_pickUpSound);
        _questItemsInventoryScript.AddItem(_itemID);
        Destroy(gameObject, _pickUpSound.length);
    }
}
