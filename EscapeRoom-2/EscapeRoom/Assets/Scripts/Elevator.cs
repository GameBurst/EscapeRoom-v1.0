using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject lever;

    public static string state;

    // Start is called before the first frame update
    void Start()
    {
        state = " (No power)";
    }
}
