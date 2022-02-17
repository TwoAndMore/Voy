using System;
using Photon.Pun;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPun
{
    [SerializeField] private PhotonView _photonView;

    [SerializeField] private int _code;

    [SerializeField] private PhotonView player;
    private void Start()
    {
      
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
            player.RequestOwnership();
        
        //_photonView.TransferOwnership(_code);
        
    }
}
