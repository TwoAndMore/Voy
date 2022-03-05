using UnityEngine;
using Photon.Pun;

public class IfIsMineEnable : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _flashlightMesh;
    
    [SerializeField] private GameObject[] _objects;

    [SerializeField] private GameObject _playerBody;
    [SerializeField] private GameObject _playerHands;
    [SerializeField] private GameObject _playerTorso;
    
    private void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        ChangeChildrenLayer(_playerBody, "PlayerBody");
        _playerHands.layer = LayerMask.NameToLayer("Hand Item");
        _playerTorso.layer = LayerMask.NameToLayer("Hand Item");
        _flashlightMesh.layer = LayerMask.NameToLayer("PlayerBody");

        StartEnable();
        ComponentsEnable();
    }

    private void StartEnable()
    {
        foreach (GameObject obj in _objects) 
            obj.SetActive(true);
    }

    private void ComponentsEnable()
    {
        GetComponent<Stamina>().enabled = true;
    }
    
    private void ChangeChildrenLayer(GameObject obj, string layerMaskName)
    {
        foreach (Transform trans in obj.GetComponentsInChildren<Transform>(true))
            trans.gameObject.layer = LayerMask.NameToLayer(layerMaskName);
    }
}
