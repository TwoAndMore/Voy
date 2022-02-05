using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private int _itemID;

    private AddItemUI _addItemUIScript;
    private Outline _outlineScript;
    private void Awake()
    {
        _outlineScript = GetComponent<Outline>();
        _addItemUIScript = GameObject.Find("Info Canvas").GetComponent<AddItemUI>();
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
        _addItemUIScript.AddItem(_itemID);
        Destroy(gameObject, _pickUpSound.length);
    }
}
