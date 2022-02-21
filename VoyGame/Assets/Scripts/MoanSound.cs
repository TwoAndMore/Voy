using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoanSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;

    private AudioSource _audioSource;
    private float _delayTime;
    private bool _isReloading;

    private void Awake() => 
        _audioSource = GetComponent<AudioSource>();

    private void Start() => 
        ChooseRandom();

    private void ChooseRandom()
    {
        if(_isReloading)
            return;
        
        _delayTime = Random.Range(5f, 15f);
        _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_delayTime);
        _isReloading = false;
        ChooseRandom();
    }
}
