using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;
    
    private bool _isPaused;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;
            SetActivePause(_isPaused);
        }
    }

    public void SetActivePause(bool status)
    {
        _pauseCanvas.SetActive(status);
        Cursor.visible = status;
    }
}
