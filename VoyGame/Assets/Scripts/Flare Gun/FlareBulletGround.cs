using UnityEngine;

public class FlareBulletGround : MonoBehaviour 
{
	[SerializeField] private AudioClip pickupSound;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (other.GetComponent<FlareGun>().currentAmmo < other.GetComponent<FlareGun>().inventorySize)
			{
				GetComponent<AudioSource>().PlayOneShot(pickupSound);			
				other.GetComponent<FlareGun>().currentAmmo++;
				Destroy(gameObject, pickupSound.length);	
			}
			else
			{
				Debug.Log("You're full");
			}
		}
	}
}

