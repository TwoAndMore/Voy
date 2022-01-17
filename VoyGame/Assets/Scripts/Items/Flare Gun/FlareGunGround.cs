using UnityEngine;

public class FlareGunGround : MonoBehaviour
{
    private const KeyCode TAKEBUTTON = KeyCode.E;
    
    [SerializeField] private AudioClip _pickupSound;
    [SerializeField] private GameObject _meshes;
    
    private GameObject _player;
    private bool _inRange;

    private void Update() => TakeLamp();

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
    
    private void TakeLamp()
    {
        if (_inRange)
        {
            if (Input.GetKeyDown(TAKEBUTTON))
            {
                GetComponent<AudioSource>().PlayOneShot(_pickupSound);
                _player.GetComponent<ItemInventory>().AddFlareGun();
                _meshes.SetActive(false);
                Destroy(gameObject, _pickupSound.length);
                _inRange = false;
            }
            //Press E icon
        }
    }
}
