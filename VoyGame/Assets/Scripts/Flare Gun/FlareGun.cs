using UnityEngine;


public class FlareGun : MonoBehaviour
{
	private const int BULLETFORCE = 2000;
	[SerializeField] private Animation _gunAnimation;
	[SerializeField] private GameObject _flareBullet;
	[SerializeField] private Transform _firePoint;
	[SerializeField] private AudioSource _gunSource;
	[SerializeField] private AudioClip[] _weaponAudio;
	[SerializeField] private GameObject _gunShootParticles;
	[SerializeField] private int _bulletsInWeaponLeft;
	
	[HideInInspector] public int inventorySize = 5;
	
	public int currentAmmo;

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
			_gunSource.PlayOneShot(_weaponAudio[1]);
			Reload();
			return;
		}

		_gunAnimation.CrossFade("Shoot");
		_gunSource.PlayOneShot(_weaponAudio[0]);
		
		_bulletsInWeaponLeft--;
		
		GameObject bulletInstance = Instantiate(_flareBullet,_firePoint.position,_firePoint.rotation); 
		bulletInstance.GetComponent<Rigidbody>().AddForce(_firePoint.forward * BULLETFORCE); 
		GameObject _bulletParticles = Instantiate(_gunShootParticles, _firePoint.position, _firePoint.rotation);
		Destroy(_bulletParticles, 1.5f);
	}
	
	private void Reload()
	{
		if(_bulletsInWeaponLeft <= 0 && currentAmmo != 0){
			_gunSource.PlayOneShot(_weaponAudio[2]);
			_gunAnimation.CrossFade("Reload");
			
			_bulletsInWeaponLeft++;
			currentAmmo--;
		}
	}
}
