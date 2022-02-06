using UnityEngine;

public class Totems : MonoBehaviour
{
    [SerializeField] private GameObject _activeEffects;
    [SerializeField] private AudioClip _activate;
    
    private QuestItemsInventory _questItemsInventoryScript;
    private bool _isActive;
    
    private void Awake() => 
        _questItemsInventoryScript = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();

    private void OnMouseDown()
    {
        if(_isActive)
            return;
        
        if (_questItemsInventoryScript.HaveElementalItems())
        {
            _isActive = true;
            _activeEffects.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(_activate);
            Destroy(GetComponent<Outline>());
            Destroy(GetComponent<OnMouseOutline>());
        }
    }
}
