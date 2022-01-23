using TMPro;
using UnityEngine;

public class GameVersion : MonoBehaviour
{
    private void Awake() => GetComponent<TextMeshProUGUI>().text = "version: " + Application.version;
}
