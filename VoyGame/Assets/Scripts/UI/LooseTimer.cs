using TMPro;
using UnityEngine;

public class LooseTimer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private TextMeshProUGUI _text;
    
    private void Awake() => 
        _text = GetComponent<TextMeshProUGUI>();

    private void FixedUpdate()
    {
        _gameManager.looseGoMenuTime -= Time.deltaTime;
        _text.text = ((int) _gameManager.looseGoMenuTime).ToString();
    }
}
