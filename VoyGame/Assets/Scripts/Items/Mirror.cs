using System.Collections;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private const KeyCode ACTIVATECODE = KeyCode.E;
    private const string DIMENSIONTAG = "AnotherDimension";

    [SerializeField] private AudioClip _delaySound;
    [SerializeField] private AudioClip _dimensionSound;
    [SerializeField] private AudioClip _goBackSound;
    [SerializeField] private AudioClip _reloadedSound;
    [SerializeField] private GameObject _particles;
    [SerializeField] private Material _glassMaterial;
    
    private Vector3 _startPosition;
    private Transform _teleportPosition;
    private AudioSource _audioSource;
    private Color32 _activeColor = new Color32(255,0,0,200);
    private Color32 _reloadColor = new Color32(70, 70, 70, 225);
    private Color32 _mainColor;
    private bool _isReloading;
    private float _reloadTime = 5f;
    private float _delayTIme = 4f;
    private float _dimensionTime = 10f;
    
    public GameObject player;
    
    private void Awake()
    {
        _mainColor = _glassMaterial.color;
        _teleportPosition = GameObject.FindWithTag(DIMENSIONTAG).transform;
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(ACTIVATECODE) && !_isReloading)
            StartCoroutine(Delay());
    }

    private void Teleport()
    {
        _startPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        _audioSource.PlayOneShot(_dimensionSound);
        ChangePlayerPosition(_teleportPosition);
        StartCoroutine(Reload());
        StartCoroutine(TimeToGoBack());
    }

    private void TeleportBack()
    {
        Transform oldPosition = player.transform;
        oldPosition.transform.position = _startPosition;
        ChangePlayerPosition(oldPosition);
    }

    private void VisualEffects(bool state, Color32 color)
    {
        _particles.SetActive(state);
        _glassMaterial.color = color;
    }

    private void ChangePlayerPosition(Transform newPosition)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = newPosition.position;
        player.GetComponent<CharacterController>().enabled = true;
    }

    private IEnumerator Delay()
    {
        VisualEffects(true, _activeColor);
        _audioSource.PlayOneShot(_delaySound);
        _isReloading = true;
        yield return new WaitForSeconds(_delayTIme);
        Teleport();
        VisualEffects(false, _mainColor);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime + _dimensionTime);
        VisualEffects(false, _mainColor);
        _audioSource.PlayOneShot(_reloadedSound);
        _isReloading = false;
    }

    private IEnumerator TimeToGoBack()
    {
        yield return new WaitForSeconds(_dimensionTime - _goBackSound.length);
        _audioSource.PlayOneShot(_goBackSound);
        VisualEffects(true, _activeColor);
        yield return new WaitForSeconds(_goBackSound.length);
        TeleportBack();
        VisualEffects(false, _reloadColor);
    }
}
