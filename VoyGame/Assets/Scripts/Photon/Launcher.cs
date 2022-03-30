using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Launcher : MonoBehaviourPunCallbacks
{
    private const string GAMEVERSION = "1";
    private const string SCENETESTNAME = "Island";
    private const string STARTPROP = "GAMESTART";
    private const byte MAXPLAYERSPERROOM = 4;
    private const int CONNECTAGAINTIME = 5;

    [SerializeField] private GameObject _music;
    [SerializeField] private GameObject _connectPanel;
    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private GameObject _roomListPanel;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private TextMeshProUGUI _lobbyTitle;
    [SerializeField] private TextMeshProUGUI _connectingText;
    [SerializeField] private TMP_InputField _nickNameField;
    [SerializeField] private Button _startButton;

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
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
        //CreateRoom();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined");

        StartCoroutine(FadingImage(_connectPanel.GetComponent<Image>()));
        _music.SetActive(true);
        _cameraAnimator.enabled = true;
        _connectingText.text = "CONNECTED";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnect");
        _connectPanel.SetActive(true);
        _connectingText.text = "ERROR\n  You may not have an internet connection. Try again.";
        StartCoroutine(ConnectAgain());
    }

    public override void OnCreatedRoom() => 
        Debug.Log("Created");

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        
        UpdatePlayerList();
        
        _roomListPanel.SetActive(false);
        _buttonPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
        _lobbyTitle.text = PhotonNetwork.CurrentRoom.Name + "'s LOBBY";

        //PhotonNetwork.LoadLevel("Test");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left");
        
        _buttonPanel.SetActive(true);
        _lobbyPanel.SetActive(false);
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Entered");
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " Left");
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) => 
        UpdateRoomList(roomList);

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.TryGetValue(STARTPROP, out bool key)) 
            LevelLoading.Instance.LoadLevel(SCENETESTNAME);
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
            
            newRoom.GetComponent<Button>().interactable = roomList[i].PlayerCount != roomList[i].MaxPlayers;
            
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
            MainMenuForms form = newPlayer.GetComponent<MainMenuForms>();
            
            _playerList.Add(newPlayer);
            
            if (Equals(PhotonNetwork.LocalPlayer, player.Value))
                form.text1.fontStyle = FontStyles.Underline | FontStyles.Bold;

            form.SetText(player.Value.NickName);
            form.image1.enabled = player.Value.IsMasterClient;
            
            _startButton.interactable = PhotonNetwork.IsMasterClient;
        }
    }
    
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = MAXPLAYERSPERROOM,
        };
        
        Hashtable roomCustomProps = new Hashtable();
        roomCustomProps.Add(STARTPROP, false);
        roomOptions.CustomRoomProperties = roomCustomProps;
        PhotonNetwork.CreateRoom(_nickNameField.text, roomOptions);
    }

    public void JoinRoom(string roomName) => 
        PhotonNetwork.JoinRoom(roomName);

    public void JoinRandom() => 
        PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => 
        PhotonNetwork.LeaveRoom();
    
    public void StartScene()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        
        _startButton.interactable = false;

        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { STARTPROP, true } });
    }

    private IEnumerator FadingImage(Image image)
    {
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
