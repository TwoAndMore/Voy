using Photon.Pun;
using UnityEngine;

public class Paper : MonoBehaviourPunCallbacks
{
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AddLetterUI _addLetterUIScript;
    
    private Outline _outlineScript;

    public int imageID;

    private void Awake() => _outlineScript = GetComponent<Outline>();

    private void OnMouseEnter() => _outlineScript.enabled = true;

    private void OnMouseExit() => _outlineScript.enabled = false;
    
    [PunRPC]
    private void OnMouseDown()
    {
        bool firstClick = false;
        if(firstClick)
            return;
        
        firstClick = true;
        
        _addLetterUIScript.AddLetter(imageID);
        GetComponent<AudioSource>().PlayOneShot(_pickUpSound);
        Destroy(gameObject, _pickUpSound.length);
    }
}