using UnityEngine;
using UnityEngine.UI;

public class SettingsPrefs : MonoBehaviour
{
    public static string FullScreen = "FullScreen";
    public static string VSync = "VSync";
    public static string TextureQuality = "TextureQuality";
    public static string AntiAliasing = "AntiAliasing";
    public static string Shadows = "Shadows";
    public static string ShadowsQuality = "ShadowsQuality";
    public static string Brightness = "Brightness";
    public static string Anisotropic = "Anisotropic";
    public static string SoftParticles = "SoftParticles";
    public static string Bloom = "Bloom";
    public static string Global = "Global";
    public static string Game = "Game";
    public static string Effects = "Effects";
    public static string Music = "Music";
    public static string FieldOfView = "FieldOfView";
    public static string MouseSensitivity = "MouseSensitivity";
    public static string PushToTalk = "PushToTalk";
    public static string ShowStatistics = "ShowStatistics";
    public static string Language = "Language";
    
    [Header("Video")]
    [SerializeField] private Dropdown _textureQuality;
    [SerializeField] private Dropdown _antiAliasing;
    [SerializeField] private Dropdown _shadows;
    [SerializeField] private Dropdown _shadowsQuality;
    [SerializeField] private Slider _brightness;
    [SerializeField] private Toggle _fullScreen;
    [SerializeField] private Toggle _VSync;
    [SerializeField] private Toggle _anisotropic;
    [SerializeField] private Toggle _softParticles;
    [SerializeField] private Toggle _bloom;

    [Header("Volume")] 
    [SerializeField] private Slider _global;
    [SerializeField] private Slider _game;
    [SerializeField] private Slider _effects;
    [SerializeField] private Slider _music;

    [Header("Game")] 
    [SerializeField] private Slider _fieldOfView;
    [SerializeField] private Slider _mouseSensitivity;
    [SerializeField] private Toggle _pushToTalk;
    [SerializeField] private Toggle _showStatistics;

    [Header("Language")] 
    [SerializeField] private Dropdown _language;
    
    //[Header("Keyboard")]
    
    private void Start() => 
        LoadSettings();

    public void LoadSettings()
    {
        SetDropdown(_textureQuality, TextureQuality);
        SetDropdown(_antiAliasing, AntiAliasing);
        SetDropdown(_shadows, Shadows);
        SetDropdown(_shadowsQuality, ShadowsQuality);
        SetDropdown(_language, Language);
        
        SetSlider(_brightness, Brightness);
        SetSlider(_global, Global);
        SetSlider(_game, Game);
        SetSlider(_effects, Effects);
        SetSlider(_music, Music);
        SetSlider(_fieldOfView, FieldOfView);
        SetSlider(_mouseSensitivity, MouseSensitivity);
        
        SetToggle(_fullScreen, FullScreen);
        SetToggle(_VSync, VSync);
        SetToggle(_anisotropic, Anisotropic);
        SetToggle(_softParticles, SoftParticles);
        SetToggle(_bloom, Bloom);
        SetToggle(_pushToTalk, PushToTalk);
        SetToggle(_showStatistics, ShowStatistics);
    }

    private void SetDropdown(Dropdown _dropdown, string pref)
    {
        if (PlayerPrefs.HasKey(pref))
            _dropdown.value = PlayerPrefs.GetInt(pref);
    }
    
    private void SetToggle(Toggle _toggle, string pref)
    {
        if (PlayerPrefs.HasKey(pref))
            _toggle.isOn = PlayerPrefs.GetInt(pref) != 0;
    }
    
    private void SetSlider(Slider _slider, string pref)
    {
        if (PlayerPrefs.HasKey(pref))
            _slider.value = PlayerPrefs.GetFloat(pref);
    }
}
