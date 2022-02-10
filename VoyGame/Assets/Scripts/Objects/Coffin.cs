using UnityEngine;

public class Coffin : MonoBehaviour
{
    private const KeyCode ACTIVATE = KeyCode.F;

    [SerializeField] private GameObject _bookPrefab;
    [SerializeField] private Transform _bookPlace;
    [SerializeField] private GameObject _bibleScreen;
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private AudioClip _putBibleDown;
    [SerializeField] private GameObject _boidButterflies;
    [SerializeField] private GameObject _boidCrews;
    [SerializeField] private GameObject _pressFIcon;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private GameObject _circleSlimsters;
    
    private QuestItemsInventory _questItemsInventoryScript;
    private GameObject _player;
    private GameObject _bookSoundObject;
    private bool _inRange;
    private bool _isReading;
    private bool _bookIsOn;
    private bool _finished;
    
    public float maxBibleProgressValue = 100f;
    public float currentBibleProgressValue;

    private void Awake() => 
        _questItemsInventoryScript = GameObject.Find("Info Canvas").GetComponent<QuestItemsInventory>();
    
    private void Update()
    {
        if(!IsObjectsActive() || !_inRange || !_bookIsOn || _finished)
            return;

        if (Input.GetKeyDown(ACTIVATE))
            ReadBible();

        if (Input.GetKeyUp(ACTIVATE))
            StopRead();
        
        if(_isReading)
            ProgressBar();
    }

    private void ReadBible()
    {
        if(_isReading)
            return;
        
        _isReading = true;

        ReadActivation(true);
    }

    private void StopRead()
    {
        if(!_bookIsOn)
            return;
        
        _isReading = false;
        
        ReadActivation(false);
    }

    private void ProgressBar()
    {
        if (currentBibleProgressValue <= maxBibleProgressValue)
        {
            _circleSlimsters.SetActive(true);
            _progressBar.SetActive(true);
            _boidCrews.SetActive(true);
            currentBibleProgressValue += Time.deltaTime / 2f;
        }
        else
        {
            StopRead();
            _circleSlimsters.SetActive(false);
            _finished = true;
            _boidCrews.SetActive(false);
            _boidButterflies.SetActive(true);
        }
    }
    private void ReadActivation(bool status)
    {
        _player.GetComponent<PlayerMovement>().enabled = !status;
        _player.transform.Find("CameraHolder").gameObject.GetComponent<MouseLook>().enabled = !status;
        _bookSoundObject.SetActive(status);
        _bibleScreen.SetActive(status);
        _bibleScreen.SetActive(status);
    }
    
    public bool IsObjectsActive()
    {
        foreach (GameObject obj in _objects)
        {
            if (obj.activeSelf == false)
                return false;
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;

        if(_bookIsOn && !_finished)
            _pressFIcon.SetActive(true);
        
        _player = other.gameObject;
        _inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        if(_bookIsOn)
            _pressFIcon.SetActive(false);

        StopRead();
        _player = null;
        _inRange = false;
    }

    private void OnMouseDown()
    {
        if(!_questItemsInventoryScript.questItemsArray[4].isFinded || _bookIsOn)
            return;
        
        _bookIsOn = true;
        Destroy(GetComponent<Outline>());
        GameObject bookObject = Instantiate(_bookPrefab, _bookPlace);
        _bookSoundObject = bookObject.transform.Find("Sound").gameObject;
        bookObject.GetComponent<AudioSource>().PlayOneShot(_putBibleDown);
    }
}
