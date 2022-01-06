using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Step()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0,_clips.Length)]);
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
    }
}
