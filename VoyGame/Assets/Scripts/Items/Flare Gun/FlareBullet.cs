using System.Collections;
using UnityEngine;

public class FlareBullet : MonoBehaviour 
{
	private const float SMOOTH = 2.4f;
	private const float FLARETIMER = 9;

	[SerializeField] private AudioClip _flareBurningSound;
	
	private Light _flareLight;
	private AudioSource _flareAudioSource;
	private ParticleSystemRenderer _smokeParticle;
	private bool _isAlive;

	private void Awake()
	{
		_flareLight = GetComponent<Light>();
		_flareAudioSource = GetComponent<AudioSource>();
		_smokeParticle = GetComponent<ParticleSystemRenderer>();
	}

	private void Start () 
	{
		GetComponent<AudioSource>().PlayOneShot(_flareBurningSound);
		StartCoroutine(TimeToFade());
		Destroy(gameObject,FLARETIMER + 1f);
	}
	
	private void Update () 
	{
		if (_isAlive)
			_flareLight.intensity = Random.Range(2f, 6f);
		else
			Fading();
	}

	private void Fading()
	{
		_flareLight.intensity =  Mathf.Lerp(_flareLight.intensity,0f,Time.deltaTime * SMOOTH);
		_flareLight.range =  Mathf.Lerp(_flareLight.range,0f,Time.deltaTime * SMOOTH);			
		_flareAudioSource.volume = Mathf.Lerp(_flareAudioSource.volume,0f,Time.deltaTime * SMOOTH);
		_smokeParticle.maxParticleSize = Mathf.Lerp(_smokeParticle.maxParticleSize,0f,Time.deltaTime * 5);
	}

	IEnumerator TimeToFade()
	{
		_isAlive = true;
		yield return new WaitForSeconds(FLARETIMER);
		_isAlive = false;
	}
}
