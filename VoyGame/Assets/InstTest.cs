using System;
using Photon.Pun;
using UnityEngine;

public class InstTest : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform thing;

    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private GameObject mainItem;

    [SerializeField] private GameObject _secondPlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (photonView.IsMine)
            {
                GameObject obj= PhotonNetwork.Instantiate("Prefabs/Bible", thing.position, Quaternion.identity );
                photonView.RPC("SetParent", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);
            }
        }
    }
    
    [PunRPC]
    private void SetParent(int thingID)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PhotonView[] thingsView = FindObjectsOfType<PhotonView>();
        
        foreach (PhotonView thing in thingsView)
        {
            if (thing.ViewID == thingID)
                mainItem = thing.gameObject;
        }
        
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().OwnerActorNr == mainItem.GetComponent<PhotonView>().OwnerActorNr)
                mainPlayer = player;
        }

        if(mainPlayer != null && mainItem != null)
            mainItem.transform.parent = mainPlayer.transform;
    }
}
