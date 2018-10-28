using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public Camera camera;

    public float speed;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float horizMov, vertMov;   
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        playerMove();
        cameraMove();
    }

    void playerMove()
    {
        //horizMov = Input.GetAxisRaw("Horizontal");
        vertMov = Input.GetAxis("Vertical");

        rb.velocity = transform.forward * speed * vertMov;
        //rb.transform.Rotate(new Vector3(0f, horizMov * 3, 0f));
        //rb.transform.Rotate(new Vector3(0, 2, 0));
    }

    void cameraMove()
    {
        if (Input.GetMouseButton(0))
        {
            float xMoveDist = speedH * Input.GetAxis("Mouse X"), yMoveDist = speedV * Input.GetAxis("Mouse Y");
            
            yaw -= xMoveDist;

            if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
                pitch += yMoveDist;

            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            rb.transform.eulerAngles = new Vector3(0f, yaw, 0f);
        }
    }
}
