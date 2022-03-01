using Photon.Pun;
using UnityEngine;

public class BookReading : MonoBehaviourPunCallbacks
{
    private const KeyCode ACTIVATE = KeyCode.F;
    private const string COFFINTAG = "Coffin";

    private CoffinEvent _coffinEventScript;
    private bool _inCoffinRange;

    private void Awake() => 
        _coffinEventScript = FindObjectOfType<CoffinEvent>();

    private void Update()
    {
        if(!photonView.IsMine || !_inCoffinRange || !_coffinEventScript.IsObjectsActivated() || !_coffinEventScript.isBookPlaced)
            return;

        if (_coffinEventScript.playerReading == null)
        {
            if(_coffinEventScript.isReading)
                return;
            
            if (Input.GetKeyDown(ACTIVATE))
                ReadStatus(true);
        }
        else
        {
            if(Input.GetKeyUp(ACTIVATE))
                ReadStatus(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(COFFINTAG) || !photonView.IsMine)
            return;
        
        _coffinEventScript.pressFIcon.SetActive(true);
        _inCoffinRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag(COFFINTAG) || !photonView.IsMine)
            return;

        if (_coffinEventScript.playerReading != null)
        {
            if (_coffinEventScript.playerReading.GetComponent<PhotonView>().IsMine) 
                ReadStatus(false);
        }
        
        _coffinEventScript.ReadObjectsActivation(false, gameObject);
        _coffinEventScript.pressFIcon.SetActive(false);
        _inCoffinRange = false;
    }

    private void ReadStatus(bool status)
    {
        if (_coffinEventScript.playerReading != null && _coffinEventScript.playerReading.TryGetComponent(out PhotonView view))
        {
            if(!view.IsMine)
                return;
        }

        photonView.RPC(status ? "StartRead" : "StopRead", RpcTarget.AllViaServer);
        _coffinEventScript.ReadObjectsActivation(status, gameObject);
    }
    
    [PunRPC]
    private void StartRead()
    {
        if(_coffinEventScript.isReading)
            return;

        _coffinEventScript.playerReading = gameObject;
        _coffinEventScript.StartReading();
    }

    [PunRPC]
    private void StopRead()
    {
        if(!_coffinEventScript.isReading)
            return;

        _coffinEventScript.playerReading = null;
        _coffinEventScript.StopReading();
    }
}
