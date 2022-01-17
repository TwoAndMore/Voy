using UnityEngine;

public class AdrenalinePills : MonoBehaviour
{
    private const KeyCode TAKEBUTTON = KeyCode.E;
    
    [SerializeField] private AudioClip _pickupSound;
    
    private GameObject _player;
    private bool _inRange;

    private void Update() => TakePills();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
            _inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _player = null;
        _inRange = false;
    }
    
    private void TakePills()
    {
        if (_inRange)
        {
            if (Input.GetKeyDown(TAKEBUTTON) && _player.GetComponent<ItemInventory>().pillsAmount < _player.GetComponent<ItemInventory>().maxPillsAmount)
            {
                GetComponent<AudioSource>().PlayOneShot(_pickupSound);
                _player.GetComponent<ItemInventory>().AddPill();
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject, _pickupSound.length);
                _inRange = false;
            }
            //Press E icon
        }
    }
}
