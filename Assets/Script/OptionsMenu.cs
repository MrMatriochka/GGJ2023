using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    
    //volume
    [Header("Volume")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeTMPUi;

    //Quality
    [Header("Volume")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    
    //Fullscreen
    [Header("Fullscreen")]
    [SerializeField] private Toggle toggleFullscreen;
    
    //Resolution
    [Header("Resolution")]
    private Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resDropdown;
    public void Awake()
    {
        VolumeSlider();
        LoadVolumeValues();
        LoadQuality();
        LoadFullscreen();
        LoadRes();
    }
    public void Save()
    {
        Time.timeScale = 1;

        //volume
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);

        //Quality
        int qualityIndex = qualityDropdown.value;
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Resolution", qualityIndex);
        
        //Resolution
        int resolutionIndex = resDropdown.value;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
        //Fullscreen
        bool isFullscreen = toggleFullscreen.isOn;
        if(isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            Screen.fullScreen = isFullscreen;
            PlayerPrefs.SetInt("Fullscreen", 0);
        }

        // LoadVolumeValues();
        // LoadQuality();
        // LoadFullscreen();
        Debug.Log(isFullscreen);
        Debug.Log(Screen.fullScreen);
        optionsPanel.SetActive(false);
        
    }
    public void VolumeSlider()
    {
        volumeTMPUi.text = volumeSlider.value.ToString("0.0");
    }
    public void LoadVolumeValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
    public void LoadQuality()
    {
        int qualityindex = PlayerPrefs.GetInt("Resolution");
        qualityDropdown.value = qualityindex;
    }
    public void LoadFullscreen()
    {
        int fullscreen = PlayerPrefs.GetInt("Fullscreen");
        if(fullscreen == 1)
        {
            toggleFullscreen.isOn = true;
        }
        else
        {
            toggleFullscreen.isOn = false;
        }
    }

    public void LoadRes()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

}
