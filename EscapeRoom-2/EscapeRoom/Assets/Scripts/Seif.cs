using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seif : MonoBehaviour {

    public TMPro.TextMeshProUGUI code, debugText;

    public GameObject inputPanel;
    public GameObject usaSeif;
    public Animator doorAnimator;

    public Camera camera;

    public AudioSource sound;

    public string rightCode;
    private string text;

    public float minDist = 5f;

    public static bool pointingAtThis, notEnteringCode;

    // Use this for initialization
    void Start () {
        code.SetText("");
        text = "";
        inputPanel.SetActive(false);
        pointingAtThis = false;
        notEnteringCode = true;
        doorAnimator.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && pointingAtThis && notEnteringCode)
        {
            checkIfTouched();
        }
	}

    public void AddDigit(int val)
    {
        sound.Play();
        print(text);
        if (text == "")
        {
            text = val.ToString();
            code.SetText(text);
            return;
        }

        if (text.Length < 4)
        {
            text += val.ToString();
            code.SetText(text);
        } else {
            //play error sound
        }
    }

    public void Cancel()
    {
        sound.Play();
        inputPanel.SetActive(false);
        code.SetText("");
        PlayerController.isPaused = false;
        Time.timeScale = 1f;
        notEnteringCode = true;
        pointingAtThis = false;
    }

    public void Ok()
    {
        sound.Play();
        //
        if (text == rightCode)
        {
            print("OK");
            debugText.SetText("OK");
            inputPanel.SetActive(false);
            doorAnimator.enabled = true;
            usaSeif.tag = "Untagged";

            PlayerController.isPaused = false;
            Time.timeScale = 1f;
            notEnteringCode = true;
            pointingAtThis = false;

            //open the door, play sound etc.
        }
        else {
            print("Not ok");
            debugText.SetText("Not OK");
            //play error sound
        }
        code.SetText("");
        text = "";
    }

    public void Delete()
    {
        sound.Play();
        if (text == "")
            return;

        if (text.Length > 0)
        {
            print(text);
            text = text.Substring(0, text.Length - 1);
            print(text);
            code.SetText(text);
        }
        else
        {
            //play error sound
        }
    }

    private void checkIfTouched()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, minDist))
        {
            print(hit.collider.name);
            print(usaSeif.name);
            //print(Input.mousePosition);
            if (hit.collider.name == usaSeif.name)
            {
                inputPanel.SetActive(true);
                PlayerController.isPaused = true;
                Time.timeScale = 0f;
                notEnteringCode = false;
                text = "";
                return;
            }
        }
    }
}
