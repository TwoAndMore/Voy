using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class LevelLoading : MonoBehaviour
{
    public static LevelLoading Instance;
    
    [SerializeField] private GameObject _loadCanvas;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadLevel(string levelName)
    {
        _loadCanvas.SetActive(true);
        PhotonNetwork.LoadLevel(levelName);

        StartCoroutine(LoadLevelAsync());
    }
    
    private IEnumerator LoadLevelAsync()
    {
        while (PhotonNetwork.LevelLoadingProgress < 1)
        {
            _text.text = "Level is Loading \n\n%" + (int)(PhotonNetwork.LevelLoadingProgress * 100);
            yield return new WaitForEndOfFrame();
        }
        
        _loadCanvas.SetActive(false);
        StopCoroutine(LoadLevelAsync());
    }
}
