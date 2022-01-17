using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    [SerializeField] private RawImage _compassImage;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _iconQuestPrefab;  
    [SerializeField] private List<QuestMarker> _questMarkers = new List<QuestMarker>();

    private float _compassUnit;
    private float _maxDistance = 30f;
    
    private void Start() => _compassUnit = _compassImage.rectTransform.rect.width / 360f;

    private void Update()
    {
        _compassImage.uvRect = new Rect(_player.localEulerAngles.y / 360f, 0f, 1f,1f);

        foreach (QuestMarker marker in _questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dst = Vector2.Distance(new Vector2(_player.transform.position.x, _player.transform.position.z),marker.position);
            float scale = 0f;

            if (dst < _maxDistance)
                scale = 1f - (dst / _maxDistance);
            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    private Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 playerFwd = new Vector2(_player.transform.forward.x, _player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(_compassUnit * angle, 0f);
    }

    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(_iconQuestPrefab, _compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;
        
        _questMarkers.Add(marker);
    }
}
