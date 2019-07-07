using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{

    public static SettingsManager Singleton { get; private set; }
    public AudioMixer Mixer;
    public GameObject m_Panel;

    public Slider music;
    public Slider radio;
    public Slider sfx;
    public Toggle fs;

    [Serializable]
    public class SSettings
    {
        public bool isFullscreen = true;
        public float musicVolume = 0f;
        public float radioVolume = 0f;
        public float SFXVolume = 0f;
    }

    SSettings sSettings = new SSettings();
    public Dropdown m_ResDropDown;

    Resolution[] resolutions;


    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        m_ResDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].Equals(Screen.currentResolution))
            {
                currentResolutionIndex = i;
            }
        }

        m_ResDropDown.AddOptions(options);
        m_ResDropDown.value = currentResolutionIndex;
        m_ResDropDown.RefreshShownValue();

        if (SaveManager.Singleton.LoadSettingsPath() != null)
        {
            sSettings = SaveManager.Singleton.LoadSettingsPath();
            ToggleFullscreen(sSettings.isFullscreen);
            fs.isOn = sSettings.isFullscreen;
            SetMusicVolume(sSettings.musicVolume);
            music.value = sSettings.musicVolume;
            SetRadioVolume(sSettings.radioVolume);
            radio.value = sSettings.radioVolume;
            SetSFXVolume(sSettings.SFXVolume);
            sfx.value = sSettings.SFXVolume;
        }

    }

    public void OpenSettings()
    {
        m_Panel.SetActive(true);
    }

    public void CloseSettings()
    {
        m_Panel.SetActive(false);
    }

    public void ToggleFullscreen(bool fullscreen)
    {
        sSettings.isFullscreen = fullscreen;
        Screen.fullScreen = fullscreen;
    }

    public void SetMusicVolume(float volume)
    {
        sSettings.musicVolume = volume;
        Mixer.SetFloat("MusicVolume", volume);
    }

    public void SetRadioVolume(float volume)
    {
        sSettings.radioVolume = volume;
        Mixer.SetFloat("RadioVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        sSettings.SFXVolume = volume;
        Mixer.SetFloat("SFXVolume", volume);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void OnApplicationQuit()
    {
        SaveManager.Singleton.SaveSettingPath(sSettings);
    }

}
