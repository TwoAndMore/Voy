using Photon.Pun;
using TMPro;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private float _statRefresh = 0.5f;
    private float _timer;

    private void Awake() =>
        _text = GetComponent<TextMeshProUGUI>();

    private void Update()
    {
        if (!(Time.unscaledTime > _timer)) 
            return;
        
        _text.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime) + "\nPING: " + PhotonNetwork.GetPing();
        _timer = Time.unscaledTime + _statRefresh;
    }
}
