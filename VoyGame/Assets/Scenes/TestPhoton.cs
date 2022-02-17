using UnityEngine;
using Photon.Pun;
using TMPro;

public class TestPhoton : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    [SerializeField] private PhotonView _photonView;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            photonView.RPC("Log", RpcTarget.All, "<3");
    }
    
    
    [PunRPC]
    private void Log(string numbers)
    {
        if(PhotonNetwork.IsMasterClient)
            _textMeshProUGUI.text += PhotonNetwork.LocalPlayer.UserId + " pressed that button" + numbers;
        else
        {
            _textMeshProUGUI.text += " ANOTHER PLAYER" + numbers;
        }
    }
    
}
