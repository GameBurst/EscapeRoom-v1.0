using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject switcher;
    public GameObject[] lights;

    public static bool isQualityLow;
    public bool entered = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("QualityLevel") == 0)
            isQualityLow = true;
        else isQualityLow = false;

        if (isQualityLow || !entered)
            turnOnAndOffTheLights(false);
        else turnOnAndOffTheLights(true);
    }

    private void Update()
    {
        if (isQualityLow)
        {
            print("Le sting");
            turnOnAndOffTheLights(false);
        } else if(!isQualityLow)
        {
            print("Le aprind");
            if(entered)
                turnOnAndOffTheLights(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("A intrat/iesit playerul " + entered);
            entered = lights[0].transform.GetChild(0).gameObject.active;
            if (!entered)
            {
                if (!isQualityLow)
                    turnOnAndOffTheLights(true);
                entered = true;
            }
            else
            {
                if (!isQualityLow)
                    turnOnAndOffTheLights(false);
                entered = false;
            }
        }
    }

    private void turnOnAndOffTheLights(bool state)
    {
        if (lights[0].transform.GetChild(0).gameObject.active == state)
            return;
        for (int i = 0; i < lights.Length; ++i)
        {
            lights[i].transform.GetChild(0).gameObject.SetActive(state);
        }
    }
}
