using UnityEngine;

public class AddMarkers : MonoBehaviour
{
    private GameObject[] _questItems;
    private Compass _compassScript;

    private void Awake() => 
        _compassScript = GetComponent<Compass>();
    
    private void Start()
    {
        _questItems = GameObject.FindGameObjectsWithTag("QuestItem");
        
        foreach (GameObject item in _questItems) 
            _compassScript.AddQuestMarker(item.GetComponent<QuestMarker>());
    }
}
