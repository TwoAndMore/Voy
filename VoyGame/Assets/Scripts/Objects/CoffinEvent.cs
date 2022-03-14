using Photon.Pun;
using UnityEngine;

public class CoffinEvent : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private GameObject _bookPrefab;
    [SerializeField] private GameObject _bibleScreen;
    [SerializeField] private GameObject _crowsBoid;
    [SerializeField] private GameObject _butterFliesBoid;
    [SerializeField] private GameObject _slimstersCircle;
    [SerializeField] private GameObject _placeEffects;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Transform _bookPlace;
    [SerializeField] private AudioClip _biblePutSound;

    private QuestItemsInventory _questItemsInventory;
    private AudioSource _audioSource;
    private bool _secondBibleActivation;
    private bool _isFinished;
    private bool _isLost;
    
    [Space(15)]
    public GameObject pressFIcon;
    public GameObject playerReading;
    public bool isReading;
    public bool isBookPlaced;
    public float maxBibleProgress = 100f;
    public float currentBibleProgress;

    private void Awake()
    {
        _questItemsInventory = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isReading && !_isFinished)
            DoProgress();
    }

    private void DoProgress()
    {
        if (!_secondBibleActivation)
        {
            _slimstersCircle.SetActive(true);
            _crowsBoid.SetActive(true);
            _placeEffects.SetActive(true);
        }
        
        _secondBibleActivation = true;

        if (currentBibleProgress <= maxBibleProgress)
            currentBibleProgress += Time.deltaTime;
        else
        {
            _isFinished = true;
            StopReading();
            _slimstersCircle.SetActive(false);
            _progressBar.SetActive(false);
            _crowsBoid.SetActive(false);
            _placeEffects.SetActive(false);
            _butterFliesBoid.SetActive(true);
        }
    }

    private void OnMouseDown() =>
        photonView.RPC("PutBible", RpcTarget.AllViaServer);

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Slimster"))
            GameLost();
    }

    [PunRPC]
    private void PutBible()
    {
        if(isBookPlaced || !_questItemsInventory.questItemsArray[4].isFinded)
            return;

        isBookPlaced = true;
        Instantiate(_bookPrefab, _bookPlace);
        _audioSource.PlayOneShot(_biblePutSound);
        _progressBar.SetActive(true);
        
        GlobalEventManager.SendBiblePut();
    }

    public void StartReading()
    {
        if(isReading || !isBookPlaced || _isFinished || _isLost)
            return;

        isReading = true;
    }
    
    public void StopReading()
    {
        if(!isBookPlaced)
            return;
        
        isReading = false;
    }

    public void GameLost()
    {
        _isLost = true;

        _slimstersCircle.SetActive(false);
        StopReading();
        
        GlobalEventManager.SendGameOver();
    }

    public void ReadObjectsActivation(bool status, GameObject player)
    {
        _audioSource.enabled = status;
        player.GetComponent<PlayerMovement>().enabled = !status;
        player.transform.Find("Main Camera").GetComponent<MouseLook>().enabled = !status;
        _bibleScreen.SetActive(status);

        if (!status)
            playerReading = null;
    }

    public bool IsObjectsActivated()
    {
        foreach (GameObject obj in _objects)
        {
            if (obj.activeSelf == false) 
                return false;
        }
        return true;
    }
}
