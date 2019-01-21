using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

    public GameObject loadingScreen;
    public GameObject pauseMenuUI;

    public static bool GameIsPaused = false;
    bool PauseOrResumeIsPressed = false;

    public Slider SensibilitySlider;
    public Dropdown GraphicsDropdown;
    public Toggle MuteUnmuteToggle;

    public GameObject globalLight;

    void Start()
    {
        //Seteaza limita de fps uri la 60
        Application.targetFrameRate = 60;

        //Verifica daca este o sensibilitate a camerei deja setata de jucator
        if (PlayerPrefs.HasKey("CameraSensibility"))
        {
            SensibilitySlider.value = PlayerPrefs.GetFloat("CameraSensibility");
        }
        else
        {
            PlayerPrefs.SetFloat("CameraSensibility", 1.0f);
            SensibilitySlider.value = 1.0f;
        }


        //Verifica daca este o grafica a jocului deja setata de jucator

        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));
            GraphicsDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        }
        else
        {
            QualitySettings.SetQualityLevel(2);
            GraphicsDropdown.value = 2;
        }

        if (GraphicsDropdown.value == 0)
            globalLight.SetActive(true);
        else globalLight.SetActive(false);

        //AudioSettings
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

        GameIsPaused = false;
        PauseOrResumeIsPressed = false;
        Time.timeScale = 1f;
    }


    void Update ()
    {
        //Verificare daca PauseMenu e activ sau nu

        if (PauseOrResumeIsPressed == true)
        {
            if (GameIsPaused == true)
            {
                Resume();
                PauseOrResumeIsPressed = false;
            }
            else
            {
                Pause();
                PauseOrResumeIsPressed = false;
            }
        }
    }


            //Functii pentru PauseMenu:

    public void PauseOrResumePress()
    {
        PauseOrResumeIsPressed = true;
    }

    void Resume ()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        PlayerController.isPaused = false;
        //Time.timeScale = 1f;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        PlayerController.isPaused = true;
        //Time.timeScale = 0f;
    }

    public void SetQuality(int qualityIndex)
    {
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);

        if(qualityIndex == 0)
        {
            globalLight.SetActive(true);
            LightSwitch.isQualityLow = true;
            //LightSwitch.set = false;
            print("LOWW");
        } else
        {
            globalLight.SetActive(false);
            LightSwitch.isQualityLow = false;
            //LightSwitch.set = true;
            print("NOTLOW");
        }
    }

    public void SetSensibility(float sensibility)
    {
        PlayerPrefs.SetFloat("CameraSensibility", sensibility);
        SensibilitySlider.value = sensibility;
    }


    //Functii pentru scene:

    public void SceneRestart ()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int level)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(level);
    }

    public void LoadNextScene()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

            //Functii pentru Audio:
            
    public void Mute (bool stare)
    {
        if(stare == true)
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
