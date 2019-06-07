using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class SettingManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumSlider;
    public GameSettings gameSettings;
    public AudioSource musicSource;
    public Button applyButton;

    public Resolution[] resolutions;
    public void Backtogame()
    {
        SceneManager.LoadScene(0);
    }
    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        LoadSettings();
    }
    public void OnFullscreenToggle()
    {
      gameSettings.fullscreen =  Screen.fullScreen = fullscreenToggle.isOn;
    }
    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
    }
    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2f, antialiasingDropdown.value);
    }
    public void OnTextureQualityChange()
    {
      QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
        
    }
    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }
    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = musicVolumSlider.value;
    }
    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings,true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);      
    }
    public void LoadSettings()
    {
        Debug.Log(Application.persistentDataPath + "/gamesettings.json");
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        vSyncDropdown.value = gameSettings.vSync;
        textureQualityDropdown.value = gameSettings.textureQuality;
        antialiasingDropdown.value = gameSettings.antialiasing;
        musicVolumSlider.value = gameSettings.musicVolume;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        Screen.fullScreen = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();
    }
    public void OnApplyButtonClick()
    {
        SaveSettings();
    }


}
