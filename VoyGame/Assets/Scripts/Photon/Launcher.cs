using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    private const string GAMEVERSION = "1";
    private const byte MAXPLAYERSPERROOM = 4;
    private const int CONNECTAGAINTIME = 5;

    [SerializeField] private GameObject _music;
    [SerializeField] private GameObject _connectPanel;
    [SerializeField] private GameObject _buttonRoomPrefab;
    [SerializeField] private GameObject _roomListContent;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private TextMeshProUGUI _connectingText;
    [SerializeField] private TMP_InputField _nickNameField;
    [SerializeField] private List<RoomInfo> _roomList;

    private void Start()
    {
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        StartCoroutine(FadingImage(_connectPanel.GetComponent<Image>()));
        _music.SetActive(true);
        _cameraAnimator.enabled = true;
        _connectingText.text = "CONNECTED";
        CreateRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _connectingText.text = "ERROR\n  You may not have an internet connection. Try again.";
        StartCoroutine(ConnectAgain());
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");
        PhotonNetwork.LoadLevel("Test");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
        ClearRoomList();

        Transform content = _roomListContent.transform;
        foreach (RoomInfo room in _roomList)
        {
            GameObject roomButton = Instantiate(_buttonRoomPrefab, content);

            roomButton.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = room.Name;
            roomButton.transform.Find("Players").GetComponent<TextMeshProUGUI>().text = room.PlayerCount + " / " + room.MaxPlayers;
            roomButton.GetComponent<Button>().onClick.AddListener(delegate
            { PhotonNetwork.JoinRoom(roomButton.transform.Find("Name").GetComponent<TextMeshProUGUI>().text); });
        }
        base.OnRoomListUpdate(roomList);
    }

    private void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = GAMEVERSION;
    }

    private void ClearRoomList()
    {
        Transform content = _roomListContent.transform;
        foreach (Transform tsf in content) 
            Destroy(tsf.gameObject);
    }
    
    public void JoinRandom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.CountOfRooms != 0)
            PhotonNetwork.JoinRandomRoom();
        else
            PhotonNetwork.CreateRoom(_nickNameField.text,  new RoomOptions(){MaxPlayers = MAXPLAYERSPERROOM, });
    }
    
    private IEnumerator FadingImage(Image image)
    {
        float colorDecrease = 1f;
        Color32 currentColor = image.color;
        if (currentColor.a <= 20)
            _connectPanel.SetActive(false);
        else
        {
            yield return new WaitForSeconds(0.005f);
            
            if (currentColor.a == 175)
                _connectingText.text = "";
            
            currentColor.a -= 1;
            image.color = currentColor;
            StartCoroutine(FadingImage(image));
        }
    }

    private IEnumerator ConnectAgain()
    {
        yield return new WaitForSeconds(3f);
        
        for (int i = CONNECTAGAINTIME; i > 0; i--)
        {
            _connectingText.text = "Connect in: " + i;
            yield return new WaitForSeconds(1f);
        }
        Connect();
    }
}
