using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchHandler : MonoBehaviour
{
    public GameObject mainLight;
    public GameObject[] lightSwitchers;

    public static bool isQualityLow;
    public static bool set;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("QualityLevel") == 0)
            isQualityLow = true;
        else isQualityLow = false;

        set = !isQualityLow;
    }

    // Update is called once per frame
    void Update()
    {
        if(isQualityLow && !set)
        {
            set = true;
            mainLight.SetActive(true);

            for(int i = 0; i < lightSwitchers.Length; ++i)
            {
                print("Sting " + lightSwitchers[i]);
                lightSwitchers[i].gameObject.GetComponent<Collider>().enabled = false;
            }
        } else if(!isQualityLow && set)
        {
            set = false;
            mainLight.SetActive(false);

            for (int i = 0; i < lightSwitchers.Length; ++i)
            {
                print("Aprind " + lightSwitchers[i]);
                lightSwitchers[i].gameObject.GetComponent<Collider>().enabled = true;
            }
        }
    }
}
