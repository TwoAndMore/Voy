using UnityEngine;

public class FootStepsAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;
    
    private void Step()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0,_clips.Length)]);
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
    }
}
