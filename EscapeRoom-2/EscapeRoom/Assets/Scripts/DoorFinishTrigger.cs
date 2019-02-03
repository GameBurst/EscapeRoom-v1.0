using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinishTrigger : MonoBehaviour {

    public Canvas UICanvas, LevelFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Checker")
        {
            UICanvas.gameObject.SetActive(false);
            LevelFinish.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
