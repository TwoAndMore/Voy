using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Dropdown _resolutionDropdown;
    
    private Resolution[] _resolutions;

    private void Start()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].Equals(Screen.currentResolution))
                currentResolutionIndex = i;
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetGlobarVolume(float volume) => 
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    
    public void SetGameVolume(float volume) => 
        _audioMixer.SetFloat("Game", Mathf.Log10(volume) * 20);
    
    public void SetEffectsVolume(float volume) => 
        _audioMixer.SetFloat("Effects", Mathf.Log10(volume) * 20);
    
    public void SetMusicVolume(float volume) => 
        _audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);

    public void SetQuality(int qualityIndex) => 
        QualitySettings.SetQualityLevel(qualityIndex);

    public void SetFullScreen(bool isFullScreen) => 
        Screen.fullScreen = isFullScreen;

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
