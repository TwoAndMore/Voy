using System.Collections;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private const KeyCode ACTIVATECODE = KeyCode.E;
    private const string DIMENSIONTAG = "AnotherDimension";
    
    private Vector3 _startPosition;
    private Transform _teleportPosition;
    private bool _isReloading;
    private float _reloadTime = 5f;
    private float _delayTIme = 1f;
    private float _dimensionTime = 3f;
    
    public GameObject player;
    
    private void Awake()
    {
        _teleportPosition = GameObject.FindWithTag(DIMENSIONTAG).transform;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(ACTIVATECODE) && !_isReloading)
            StartCoroutine(Delay());
    }

    private void Teleport()
    {
        _startPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
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

    private void VisualEffects()
    {
        
    }

    private void ChangePlayerPosition(Transform newPosition)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = newPosition.position;
        player.GetComponent<CharacterController>().enabled = true;
    }

    private IEnumerator Delay()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_delayTIme);
        Teleport();
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime + _dimensionTime);
        _isReloading = false;
    }

    private IEnumerator TimeToGoBack()
    {
        yield return new WaitForSeconds(_dimensionTime);
        TeleportBack();
    }
}
