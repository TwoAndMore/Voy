using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Dropdown _resolutionDropdown;
    [SerializeField] private GameObject _statisticCanvas;
    [SerializeField] private PostProcessVolume _postProcessVolume;
    
    private Resolution[] _resolutions;
    private Bloom _bloomEffect;
    private ColorGrading _colorGradingEffect;

    private void Awake()
    {
        FindAllResolutions();
        
        _postProcessVolume.profile.TryGetSettings(out _bloomEffect);
        _postProcessVolume.profile.TryGetSettings(out _colorGradingEffect);
    }

    private void Start() => 
        gameObject.SetActive(false);

    private void FindAllResolutions()
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
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt(SettingsPrefs.FullScreen, isFullScreen ? 1 : 0);
    }

    public void SetVSync(bool isVSync)
    {
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
        PlayerPrefs.SetInt(SettingsPrefs.VSync, isVSync ? 1 : 0);
    }

    public void SetTextureQuality(int index)
    {
        QualitySettings.masterTextureLimit = index;
        PlayerPrefs.SetInt(SettingsPrefs.TextureQuality, index);
    }

    public void SetAntiAliasing(int index)
    {
        QualitySettings.antiAliasing = index switch
        {
            0 => 0,
            1 => 2,
            2 => 4,
            3 => 8,
            _ => 0,
        };
        PlayerPrefs.SetInt(SettingsPrefs.AntiAliasing, index);
    }
    
    public void SetShadows(int index)
    {
        QualitySettings.shadows = index switch
        {
            0 => ShadowQuality.Disable,
            1 => ShadowQuality.HardOnly,
            2 => ShadowQuality.All,
            _ => ShadowQuality.Disable
        };
        PlayerPrefs.SetInt(SettingsPrefs.Shadows, index);
    }

    public void SetShadowsQuality(int index)
    {
        QualitySettings.shadowResolution = index switch
        {
            0 => ShadowResolution.Low,
            1 => ShadowResolution.Medium,
            2 => ShadowResolution.High,
            3 => ShadowResolution.VeryHigh,
            _ => ShadowResolution.Low,
        };
        PlayerPrefs.SetInt(SettingsPrefs.ShadowsQuality, index);
    }

    public void SetBrightness(float index)
    {
        if(_colorGradingEffect == null)
            return;
        
        _colorGradingEffect.gamma.value.w = index;
        PlayerPrefs.SetFloat(SettingsPrefs.Brightness, index);
    }
    
    public void SetAnisotropic(bool status)
    {
        QualitySettings.anisotropicFiltering = status ? AnisotropicFiltering.ForceEnable : AnisotropicFiltering.Disable;
        PlayerPrefs.SetInt(SettingsPrefs.Anisotropic, status ? 1 : 0);
    }
    
    public void SetSoftParticles(bool status)
    {
        QualitySettings.softParticles = status;
        PlayerPrefs.SetInt(SettingsPrefs.SoftParticles, status ? 1 : 0);
    }
    
    public void SetBloom(bool status)
    {
        if(_bloomEffect == null)
            return;
        
        _bloomEffect.active = status;
        PlayerPrefs.SetInt(SettingsPrefs.Bloom, status ? 1 : 0);
    }

    public void SetGlobalVolume(float volume)
    {
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SettingsPrefs.Global, volume);
    }

    public void SetGameVolume(float volume)
    {
        _audioMixer.SetFloat("Game", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SettingsPrefs.Game, volume);
    }

    public void SetEffectsVolume(float volume)
    {
        _audioMixer.SetFloat("Effects", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SettingsPrefs.Effects, volume);
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SettingsPrefs.Music, volume);
    }

    public void SetFOV(float index)
    {
        PlayerStats.FieldOfView = (int)index;
        PlayerPrefs.SetFloat(SettingsPrefs.FieldOfView, index);
    }
    
    public void SetMouseSensitivity(float index)
    {
        PlayerStats.MouseSensitivity = index;
        PlayerPrefs.SetFloat(SettingsPrefs.MouseSensitivity, index);
    }
    
    public void SetPushToTalk(bool status)
    {
        PlayerPrefs.SetInt(SettingsPrefs.PushToTalk, status ? 1 : 0);
    }
    
    public void SetShowStatistics(bool status)
    {
        _statisticCanvas.SetActive(status);
        PlayerPrefs.SetInt(SettingsPrefs.ShowStatistics, status ? 1 : 0);
    }

    public void SetLanguage(int index)
    {
        PlayerPrefs.SetInt(SettingsPrefs.Language, index);
    }
    
    public void SetQuality(int qualityIndex) => 
        QualitySettings.SetQualityLevel(qualityIndex);
}
