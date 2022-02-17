using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private const string PLAYERPREFABPATH = "Player/Player";
    
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private TextMeshProUGUI _logText;
    
    public static GameManager Instance;

    private void Start()
    {
        //PhotonNetwork.OfflineMode = true;
        Instance = this;
        Spawn();
    }

    private void Spawn()
    {
        Transform spawn = _spawnPoints[0];
        PhotonNetwork.Instantiate(PLAYERPREFABPATH, spawn.position, Quaternion.identity);
    }

    private void Log(string text) => 
        _logText.text = "\n" + text;

    public override void OnLeftRoom() => 
        SceneManager.LoadScene("MainMenu");

    public override void OnPlayerLeftRoom(Player otherPlayer) => 
        Log(otherPlayer.NickName + "Disconnected");

    public void LeaveButton() =>
        PhotonNetwork.LeaveRoom();
}
