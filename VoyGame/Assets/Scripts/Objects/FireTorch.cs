using UnityEngine;

public class FireTorch : MonoBehaviour
{
    [SerializeField] private GameObject _fireLight;
    [SerializeField] private AudioClip _activate;
    
    private QuestItemsInventory _questItemsInventoryScript;
    private bool _isActive;

    private void Awake() => 
        _questItemsInventoryScript = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();

    private void OnMouseDown()
    {
        if(!_questItemsInventoryScript.questItemsArray[6].isFinded || _isActive)
            return;
        
        _isActive = true;
        _fireLight.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(_activate);
        Destroy(GetComponent<Outline>());
        Destroy(GetComponent<OnMouseOutline>());
    }
}
