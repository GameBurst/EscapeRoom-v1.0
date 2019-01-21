using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject switcher;
    public GameObject[] lights;

    public static bool isQualityLow, set;
    private bool lightsOn;

    private void Start()
    {
        for (int i = 0; i < lights.Length; ++i)
        {
            lights[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("QualityLevel") == 0)
            isQualityLow = set = lightsOn = true;
        else isQualityLow = set = lightsOn = false;
    }

    private void Update()
    {
        if (!switcher.GetComponent<BoxCollider>().enabled)
            checkLights();
    }

    private void checkLights()
    {
        //print(isQualityLow + " " + set);
        if (isQualityLow && !set)
        {
            print(switcher);
            switcher.GetComponent<BoxCollider>().enabled = false;
            for (int i = 0; i < lights.Length; ++i)
            {
                lights[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            
            set = true;
        }
        else if (!isQualityLow && set)
        {
            set = false;
            switcher.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isQualityLow)
            {
                print(switcher);
                //print("MATAA");
                for (int i = 0; i < lights.Length; ++i)
                {
                    lights[i].transform.GetChild(0).gameObject.SetActive(true);
                }

                lightsOn = true;
            }
            else { print("OK"); set = false; checkLights(); }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (!isQualityLow)
            {
                print(switcher);
                //print("MA TA WA");
                //print("MA TA WA");
                for (int i = 0; i < lights.Length; ++i)
                {
                    lights[i].transform.GetChild(0).gameObject.SetActive(true);
                }

                lightsOn = true;
            }
            else { print("OK"); set = false; checkLights(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isQualityLow || other.gameObject.tag == "Player")
        {
            for (int i = 0; i < lights.Length; ++i)
            {
                lights[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            lightsOn = false;
        }
    }
}
