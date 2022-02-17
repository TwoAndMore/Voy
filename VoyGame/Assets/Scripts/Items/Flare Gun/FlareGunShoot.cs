using UnityEngine;
using Photon.Pun;

public class FlareGunShoot : MonoBehaviourPunCallbacks
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
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
        if(Input.GetMouseButtonDown(1) && !_gunAnimation.isPlaying) 
            photonView.RPC("Shoot", RpcTarget.All);
        if(Input.GetKeyDown(KeyCode.R) && !_gunAnimation.isPlaying) 
            photonView.RPC("Reload", RpcTarget.All);
    }
  
    [PunRPC]
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
    
    [PunRPC]
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
