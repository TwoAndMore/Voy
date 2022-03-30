using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private const string MENUSCENENAME = "MainMenu";
    
    [SerializeField] private GameObject _pauseCanvas;

    public static bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            SetActivePause();
    }

    public void SetActivePause()
    {
        isPaused = !isPaused;
        _pauseCanvas.SetActive(isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
        LevelLoading.Instance.LoadLevel(MENUSCENENAME);
    }
}
