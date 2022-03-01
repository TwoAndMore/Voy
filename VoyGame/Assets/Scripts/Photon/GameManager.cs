using System;
using System.Collections;
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
    [SerializeField] private GameObject _gameOverScreen;
    
    public static GameManager Instance;

    public float looseGoMenuTime = 15f;

    private void Awake() => 
        GlobalEventManager.OnGameOver.AddListener(GameOverYouLost);

    private void Start()
    {
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

    private void GameOverYouLost()
    {
        Debug.Log("asoidasjdsuhsadashdadshoiaoashashoi");
        _gameOverScreen.SetActive(true);
        StartCoroutine(YouLost());
    }

    private IEnumerator YouLost()
    {
        yield return new WaitForSeconds(looseGoMenuTime);
        PhotonNetwork.LoadLevel("MainMenu");
    }
    
    public override void OnLeftRoom() => 
        SceneManager.LoadScene("MainMenu");

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //Log(otherPlayer.NickName + "Disconnected");
    }

    public void LeaveButton() =>
        PhotonNetwork.LeaveRoom();
}
