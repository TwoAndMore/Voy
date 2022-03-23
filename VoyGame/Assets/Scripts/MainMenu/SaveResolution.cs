using UnityEngine;

public class SaveResolution : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.R) && Input.GetKeyDown(KeyCode.G))
            Screen.SetResolution(1920, 1080, true);
    }
}
