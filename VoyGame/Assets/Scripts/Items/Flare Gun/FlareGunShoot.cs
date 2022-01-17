using UnityEngine;

public class FlareGunShoot : MonoBehaviour
{
    private const int BULLETFORCE = 2000;

    [SerializeField] private GameObject _flareBullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AudioClip[] _weaponAudio;
    [SerializeField] private GameObject _gunShootParticles;

    private AudioSource _audioSource;
    private Animation _gunAnimation;
    private int _bulletsInWeaponLeft;

    public ItemInventory itemInventoryScript;
    public FlareGunAmmoText flareGunAmmoTextScript;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _gunAnimation = GetComponent<Animation>();
    }

    private void Update () 
    {
        if(Input.GetMouseButtonDown(0) && !_gunAnimation.isPlaying) 
            Shoot();
        if(Input.GetKeyDown(KeyCode.R) && !_gunAnimation.isPlaying) 
            Reload();
    }
  
    private void Shoot()
    {
        if (_bulletsInWeaponLeft <= 0)
        {
            _gunAnimation.Play("noAmmo");
            _audioSource.PlayOneShot(_weaponAudio[1]);
            Reload();
            return;
        }

        _gunAnimation.CrossFade("Shoot");
        _audioSource.PlayOneShot(_weaponAudio[0]);
    
        _bulletsInWeaponLeft--;

        GameObject bulletInstance = Instantiate(_flareBullet,_firePoint.position,_firePoint.rotation); 
        bulletInstance.GetComponent<Rigidbody>().AddForce(_firePoint.forward * BULLETFORCE); 
        GameObject _bulletParticles = Instantiate(_gunShootParticles, _firePoint.position, _firePoint.rotation);
        Destroy(_bulletParticles, 1.5f);
    }
  
    private void Reload()
    {
        if(_bulletsInWeaponLeft <= 0 && itemInventoryScript.currentAmmoAmount != 0){
            _audioSource.PlayOneShot(_weaponAudio[2]);
            _gunAnimation.CrossFade("Reload");
            _bulletsInWeaponLeft++;
            itemInventoryScript.currentAmmoAmount--;
            flareGunAmmoTextScript.SetText();
        }
    }
}
