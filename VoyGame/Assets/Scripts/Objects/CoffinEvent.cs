using System.Security.Cryptography;
using UnityEngine;

public class CoffinEvent : MonoBehaviour
{
    private const KeyCode ACTIVATE = KeyCode.F;
    private const string PLAYERTAG = "Player";

    [SerializeField] private GameObject[] _objects;
    [SerializeField] private GameObject _bookPrefab;
    [SerializeField] private AudioClip _biblePutSound;
    
    private QuestItemsInventory _questItemsInventory;
    private AudioSource _audioSource;
    private GameObject _player;
    private GameObject _bibleScreen;
    private GameObject _pressFIcon;
    private GameObject _crowsBoid;
    private GameObject _butterFlisBoid;
    private GameObject _slimstersCircle;
    private GameObject _progressBar;
    private Transform _bookPlace;
    private bool _inRange;
    private bool _isReading;
    private bool _isBookPlaced;
    private bool _isFinished;
    private bool _secondBibleActivation;
    private bool _isLost;

    public float maxBibleProgress = 100f;
    public float currentBibleProgress = 0f;

    private void Awake()
    {
        _questItemsInventory = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
        _bibleScreen = transform.Find("BibleUI/BookImage").gameObject;
        _progressBar = transform.Find("BibleUI/Bible Slider").gameObject;
        _pressFIcon = transform.Find("F Icon Canvas").gameObject;
        _bookPlace = transform.Find("BiblePlace");
        _crowsBoid = transform.Find("Boid Crows").gameObject;
        _butterFlisBoid = transform.Find("Boid ButterFlies").gameObject;
        _slimstersCircle = transform.Find("Slimsters Circle").gameObject;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!IsObjectsActivated() || !_inRange || !_isBookPlaced || _isFinished || _isLost)
            return;
        
        if(Input.GetKeyDown(ACTIVATE))
            StartReading();
        
        if(Input.GetKeyUp(ACTIVATE))
            StopReading();

        if (_isReading && !_isFinished)
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
            _butterFlisBoid.SetActive(true);
        }
    }

    private void OnMouseDown() => 
        PutBible();

    private void OnTriggerEnter(Collider other) => 
        TriggerCheck(other.gameObject, true);

    private void OnTriggerExit(Collider other) => 
        TriggerCheck(other.gameObject, false);

    private void StartReading()
    {
        if(_isReading)
            return;

        _isReading = true;
        ReadObjectsActivation(true);
    }

    private void StopReading()
    {
        if(!_isBookPlaced)
            return;

        _isReading = false;
        ReadObjectsActivation(false);
    }

    private void ReadObjectsActivation(bool status)
    {
        _player.GetComponent<PlayerMovement>().enabled = !status;
        _player.transform.Find("CameraHolder").GetComponent<MouseLook>().enabled = !status;
        _bibleScreen.SetActive(status);
        _audioSource.enabled = status;
    }

    private void PutBible()
    {
        if(_isBookPlaced || !_questItemsInventory.questItemsArray[4].isFinded || !_inRange)
            return;

        _isBookPlaced = true;
        Instantiate(_bookPrefab, _bookPlace);
        _audioSource.PlayOneShot(_biblePutSound);
        _progressBar.SetActive(true);
    }

    private void GameLost()
    {
        _isLost = true;
        StopReading();
    }

    private void TriggerCheck(GameObject obj, bool isEnter)
    {
        if(!obj.CompareTag(PLAYERTAG))
            return;

        if (isEnter)
        {
            if(_isBookPlaced && !_isFinished)
                _pressFIcon.SetActive(true);
        }
        else
        {
            StopReading();
            _pressFIcon.SetActive(false);
        }
        
        _inRange = isEnter;
        _player = isEnter ? obj : null;
    }

    private bool IsObjectsActivated()
    {
        foreach (GameObject obj in _objects)
        {
            if (obj.activeSelf == false) 
                return false;
        }
        return true;
    }
}
