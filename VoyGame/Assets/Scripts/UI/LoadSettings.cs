using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    [SerializeField] private SettingsPrefs _settingsPrefs;
    private void Start() => 
        _settingsPrefs.LoadSettings();
}
