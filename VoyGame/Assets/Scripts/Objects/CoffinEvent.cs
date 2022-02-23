using Photon.Pun;
using UnityEngine;

public class CoffinEvent : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private GameObject _bookPrefab;
    [SerializeField] private AudioClip _biblePutSound;
    
    private QuestItemsInventory _questItemsInventory;
    private AudioSource _audioSource;
    private GameObject _bibleScreen;
    private GameObject _crowsBoid;
    private GameObject _butterFliesBoid;
    private GameObject _slimstersCircle;
    private GameObject _progressBar;
    private Transform _bookPlace;
    private bool _secondBibleActivation;
    private bool _isFinished;
    private bool _isLost;

    public GameObject pressFIcon;
    public GameObject playerReading;
    public bool isReading;
    public bool isBookPlaced;
    public float maxBibleProgress = 100f;
    public float currentBibleProgress;

    private void Awake()
    {
        _questItemsInventory = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
        _bibleScreen = transform.Find("BibleUI/BookImage").gameObject;
        _progressBar = transform.Find("BibleUI/Bible Slider").gameObject;
        pressFIcon = transform.Find("F Icon Canvas").gameObject;
        _bookPlace = transform.Find("BiblePlace");
        _crowsBoid = transform.Find("Boid Crows").gameObject;
        _butterFliesBoid = transform.Find("Boid ButterFlies").gameObject;
        _slimstersCircle = transform.Find("Slimsters Circle").gameObject;
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
            _butterFliesBoid.SetActive(true);
        }
    }

    private void OnMouseDown() =>
        photonView.RPC("PutBible", RpcTarget.AllViaServer);

    [PunRPC]
    private void PutBible()
    {
        if(isBookPlaced || !_questItemsInventory.questItemsArray[4].isFinded)
            return;

        isBookPlaced = true;
        Instantiate(_bookPrefab, _bookPlace);
        _audioSource.PlayOneShot(_biblePutSound);
        _progressBar.SetActive(true);
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
        StopReading();
    }

    public void ReadObjectsActivation(bool status, GameObject player)
    {
        _audioSource.enabled = status;
        player.GetComponent<PlayerMovement>().enabled = !status;
        player.transform.Find("CameraHolder").GetComponent<MouseLook>().enabled = !status;
        _bibleScreen.SetActive(status);

        if (!status)
            this.playerReading = null;
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
