using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

    public GameObject[] lights;

    private void Start()
    {
        for (int i = 0; i < lights.Length; ++i)
        {
            lights[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            for(int i = 0; i < lights.Length; ++i)
            {
                lights[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < lights.Length; ++i)
            {
                lights[i].SetActive(false);
            }
        }
    }
}
