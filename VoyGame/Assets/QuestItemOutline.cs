using UnityEngine;

public class QuestItemOutline : MonoBehaviour
{
    [SerializeField] private int _itemID;
    [SerializeField] private GameObject _player;

    private QuestItemsInventory _questItemsInventory;
    private Outline _outline;
    private AudioSource _audioSource;
    private Collider _collider;
    private bool _inRange;
    private bool _firstClick;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _questItemsInventory = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
        _audioSource = GetComponent<AudioSource>();
        _outline = GetComponent<Outline>();
    }

    private void OnTriggerEnter(Collider other) => 
        SetTrigger(true, other.gameObject);

    private void OnTriggerExit(Collider other) => 
        SetTrigger(false, other.gameObject);

    private void OnMouseEnter()
    {
        if (_inRange)
            _outline.enabled = true;
    }

    private void OnMouseExit() => 
        _outline.enabled = false;

    private void OnMouseDown()
    {
        if(!_inRange)
            return;
        
        _collider.enabled = false;
        _questItemsInventory.AddItem(_itemID);
        _audioSource.Play();
        Destroy(gameObject, 0.29f);
    }

    private void SetTrigger(bool status, GameObject obj)
    {
        if (!obj.CompareTag("Player")) 
            return;
        
        _inRange = status;

        _player = status ? obj : null;
    }
}
