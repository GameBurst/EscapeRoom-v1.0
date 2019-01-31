using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject switcher;
    public GameObject[] lights;

    public static bool isQualityLow, set;

    private void Start()
    {
        turnOnOrOffTheLights(false);
    }

    private void Update()
    {
        if (!switcher.GetComponent<BoxCollider>().enabled)
            turnOnOrOffTheLights(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            turnOnOrOffTheLights(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            turnOnOrOffTheLights(false);
        }
    }

    private void turnOnOrOffTheLights(bool state)
    {
        for (int i = 0; i < lights.Length; ++i)
        {
            lights[i].transform.GetChild(0).gameObject.SetActive(state);
        }
    }
}
