using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;
    
    private bool _isPaused = true;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            SetActivePause(_isPaused);
    }

    public void SetActivePause(bool status)
    {
        _isPaused = !_isPaused;
        _pauseCanvas.SetActive(status);
        Cursor.lockState = status ? CursorLockMode.None : CursorLockMode.Confined;
        Cursor.visible = status;
    }
}
