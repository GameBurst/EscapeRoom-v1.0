using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    void Awake()
    {
        int nrAudioSources = FindObjectsOfType<AudioSource>().Length;
        if (nrAudioSources != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
