using System.Collections.Generic;
using UnityEngine;

public class AddMarkers : MonoBehaviour
{
    [SerializeField] private List<QuestMarker> _startMarkers = new List<QuestMarker>();

    private Compass _compassScript;

    private void Awake() => _compassScript = GetComponent<Compass>();
    
    private void Start() => AddAllMarkersInList(_startMarkers);

    private void AddAllMarkersInList(List<QuestMarker> questMarkersList)
    {
        foreach (QuestMarker marker in questMarkersList) 
            _compassScript.AddQuestMarker(marker);
    }
}
