using System.Collections;
using TMPro;
using UnityEngine;

public class LooseTimer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _text;
    
    private void Start()
    {
        StartCoroutine(TimerUpdate());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator TimerUpdate()
    {
        for (int i = _gameManager.looseGoMenuTime; i > 0; i--)
        {
            _text.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
    }
}
