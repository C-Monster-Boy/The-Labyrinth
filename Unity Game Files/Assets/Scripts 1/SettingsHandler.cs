using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{

    public AudioMixer mixer;
    public TMPro.TMP_Dropdown resoDropdown;
    public Slider xSensSlider;
    public Slider ySensSlider;
    public Text xSensValue;
    public Text ySensValue;

    private Resolution[] reso;
    private int currentResolutionIndex;

    void Start()
    {
        reso = Screen.resolutions;
        List<string> resoList = new List<string>();
        resoDropdown.ClearOptions();

        for (int i=0; i<reso.Length;i++)
        {
            if (reso[i].width / reso[i].height == 16 / 9)
            {
                string temp = reso[i].width + "X" + reso[i].height;
                resoList.Add(temp);
            }
            if(reso[i].width == Screen.currentResolution.width && reso[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resoDropdown.AddOptions(resoList);
        resoDropdown.value = currentResolutionIndex;
        resoDropdown.RefreshShownValue();

        xSensSlider.value = PlayerPrefs.GetFloat("X_SENS", 300);
        ySensSlider.value = PlayerPrefs.GetFloat("Y_SENS", 2f);

        xSensValue.text = Mathf.Ceil(xSensSlider.value).ToString();
        ySensValue.text = Mathf.Ceil(ySensSlider.value * 100f).ToString();
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("MasterVolume",volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool fullScreenVar)
    {
        Screen.fullScreen = fullScreenVar;
    }

    public void SetResolution(int resIndex)
    {
        Resolution r = reso[resIndex];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }

    public void Set_X_Sensitivity(float xSens)
    {
        PlayerPrefs.SetFloat("X_SENS", xSens);
        xSensValue.text = Mathf.Ceil(xSens).ToString();
       
    }

    public void Set_Y_Sensitivity(float ySens)
    {
        PlayerPrefs.SetFloat("Y_SENS", ySens);
        ySensValue.text = Mathf.Ceil(ySens * 100f).ToString();
    }
}
