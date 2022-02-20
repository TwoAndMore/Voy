using Photon.Pun;
using UnityEngine;

public class ObjectOutline : MonoBehaviourPunCallbacks
{
    private Outline _outline;

    private void Awake() => 
        _outline = GetComponent<Outline>();

    private void OnTriggerEnter(Collider other) =>
        SetOutline(true, other);
    
    private void OnTriggerExit(Collider other) =>
        SetOutline(false, other);

    private void SetOutline(bool status, Collider some)
    {
        if(!some.CompareTag("Player") || !some.gameObject.GetComponent<PhotonView>().IsMine)
            return;
        
        _outline.enabled = status;
    }
}
