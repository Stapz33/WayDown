using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public static SettingsManager Singleton { get; private set; }
    public AudioMixer Mixer;
    public GameObject m_Panel;

    public Dropdown m_ResDropDown;

    Resolution[] resolutions;


    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(this);
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
        Screen.fullScreen = fullscreen;
    }

    public void SetMusicVolume(float volume)
    {
        Mixer.SetFloat("MusicVolume", volume);
    }

    public void SetRadioVolume(float volume)
    {
        Mixer.SetFloat("RadioVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        Mixer.SetFloat("SFXVolume", volume);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


}
