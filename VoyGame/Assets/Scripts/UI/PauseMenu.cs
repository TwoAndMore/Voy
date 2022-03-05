using UnityEngine;

public class PauseMenu : MonoBehaviour
{
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
}
