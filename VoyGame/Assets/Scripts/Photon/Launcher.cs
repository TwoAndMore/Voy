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
    private const string SCENETESTNAME = "Test";
    private const byte MAXPLAYERSPERROOM = 4;
    private const int CONNECTAGAINTIME = 5;

    [SerializeField] private GameObject _music;
    [SerializeField] private GameObject _connectPanel;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _roomListPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _lobbyTitle;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private TextMeshProUGUI _connectingText;
    [SerializeField] private TMP_InputField _nickNameField;
    
    [Header("FORMS")][Space(15)]
    [SerializeField] private GameObject _newRoomFormPrefab;
    [SerializeField] private GameObject _newPlayerFormPrefab;
    [SerializeField] private Transform _roomFormParent;
    [SerializeField] private Transform _playerFormParent;
    [SerializeField] private List<GameObject> _roomList = new List<GameObject>();
    [SerializeField] private List<GameObject> _playerList = new List<GameObject>();

    private void Start() => 
        Connect();

    public override void OnConnectedToMaster()
    {
        StartCoroutine(FadingImage(_connectPanel.GetComponent<Image>()));
        _music.SetActive(true);
        _cameraAnimator.enabled = true;
        _connectingText.text = "CONNECTED";
        PhotonNetwork.JoinLobby();
        //CreateRoom();
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
        UpdatePlayerList();
        
        _roomListPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
        _lobbyTitle.text = PhotonNetwork.CurrentRoom.Name + "'s LOBBY";

        //PhotonNetwork.LoadLevel("Test");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left");
        UpdatePlayerList();
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        Debug.Log(newPlayer.NickName + " Entered");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        Debug.Log(otherPlayer.NickName + " Left");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    private void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = GAMEVERSION;
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        for(int i = 0; i < _roomList.Count; i++)
            Destroy(_roomList[i]);
        
        _roomList.Clear();
        
        for (int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].PlayerCount == 0)
                continue;

            GameObject newRoom = Instantiate(_newRoomFormPrefab, _roomFormParent.transform);
            newRoom.GetComponent<MainMenuForms>().SetRoomValues(roomList[i].Name, roomList[i].PlayerCount + " / " + roomList[i].MaxPlayers);
            newRoom.GetComponent<Button>().onClick.AddListener(delegate
            {
                JoinRoom(newRoom.GetComponent<MainMenuForms>().text1.text);
            });
            
            _roomList.Add(newRoom);
        }
    }

    private void UpdatePlayerList()
    {
        for (int i = 0; i <_playerList.Count; i++) 
            Destroy(_playerList[i]);
    
        _playerList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
            return;
        
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            GameObject newPlayer = Instantiate(_newPlayerFormPrefab, _playerFormParent);
            
            _playerList.Add(newPlayer);
            
            newPlayer.GetComponent<MainMenuForms>().SetText(player.Value.NickName);
            
            if (Equals(PhotonNetwork.LocalPlayer, player.Value))
                newPlayer.GetComponent<MainMenuForms>().text1.fontStyle = FontStyles.Underline | FontStyles.Bold;
            
            _startButton.interactable = PhotonNetwork.IsMasterClient;
        }
    }

    public void CreateRoom() => 
        PhotonNetwork.CreateRoom(_nickNameField.text,  new RoomOptions{MaxPlayers = MAXPLAYERSPERROOM, IsVisible = true});

    public void JoinRoom(string roomName) => 
        PhotonNetwork.JoinRoom(roomName);

    public void JoinRandom() => 
        PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => 
        PhotonNetwork.LeaveRoom();

    public void StartScene() => 
        PhotonNetwork.LoadLevel(SCENETESTNAME);

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
