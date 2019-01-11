using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider SensibilitySlider;
    public Dropdown GraphicsDropdown;

    public Toggle MuteUnmuteToggle;

    void Start()
    {
        //Camera Sensibility
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            SensibilitySlider.value = PlayerPrefs.GetFloat("CameraSensibility");
        }
        else
        {
            SensibilitySlider.value = 1.0f;
        }
            

        //Graphics quality
        if(PlayerPrefs.HasKey("QualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));
            GraphicsDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        }
        else
        {
            QualitySettings.SetQualityLevel(2);
            GraphicsDropdown.value = 2;
        }

        //Audio settings
        if (PlayerPrefs.HasKey("Muted"))
        {
            if (PlayerPrefs.GetInt("Muted") == 1)
            {
                MuteUnmuteToggle.isOn = true;
                AudioListener.pause = true;
            }
            else
            {
                MuteUnmuteToggle.isOn = false;
                AudioListener.pause = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            MuteUnmuteToggle.isOn = false;
            AudioListener.pause = false;
        }

    }

	public void  SetQuality(int qualityIndex)
	{
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);       
    }
	
	public void SetSensibility(float sensibility)
	{
        PlayerPrefs.SetFloat("CameraSensibility", sensibility);
        SensibilitySlider.value = sensibility;
    }

    public void Mute(bool stare)
    {
        if (stare == true)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }

        MuteUnmuteToggle.isOn = stare;
        AudioListener.pause = stare;
    }
}
