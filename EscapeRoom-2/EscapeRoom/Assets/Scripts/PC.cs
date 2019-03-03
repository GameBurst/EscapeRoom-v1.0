using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    Animator animator;

    bool started;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.enabled)
        {

        }
    }
}
