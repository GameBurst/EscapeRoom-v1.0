using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinishTrigger : MonoBehaviour {

    public Canvas HUD, gameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Checker")
        {
            HUD.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
