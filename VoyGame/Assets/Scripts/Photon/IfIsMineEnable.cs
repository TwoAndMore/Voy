using UnityEngine;
using Photon.Pun;

public class IfIsMineEnable : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _flashlightMesh;
    
    [SerializeField] private GameObject[] _objects;
    
    private GameObject _playerBody;

    private void Awake()
    {
        _playerBody = transform.Find("Body").gameObject;
    }

    private void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
    
        ChangeChildrenLayer(_playerBody, "PlayerBody");
        _flashlightMesh.layer = LayerMask.NameToLayer("PlayerBody");

        StartEnable();
    }

    private void StartEnable()
    {
        foreach (GameObject obj in _objects) 
            obj.SetActive(true);
    }

    private void ChangeChildrenLayer(GameObject obj, string layerMaskName)
    {
        foreach (Transform trans in obj.GetComponentsInChildren<Transform>(true))
            trans.gameObject.layer = LayerMask.NameToLayer(layerMaskName);
    }
}
