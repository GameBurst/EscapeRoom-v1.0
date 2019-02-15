using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : InterObjjj
{
    public GameObject obj;
    public Animator animator;

    // Use this for initialization
    void Start()
    {
        if (animator != null)
            animator.enabled = false;
    }

    public override void Activate()
    {
        obj.tag = "Untagged";
        if (animator != null)
        {
            print("Enabling");
            animator.enabled = true;
        }
        //else obj.GetComponent<Rigidbody>().velocity = force;
        Elevator.state = " (No button)";
    }
}
