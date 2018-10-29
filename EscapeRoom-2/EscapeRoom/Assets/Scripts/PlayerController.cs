using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public Camera camera;

    public Text interactableName;

    public float minDist;
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
        checkForHit();
    }

    void playerMove()
    {
        //horizMov = Input.GetAxisRaw("Horizontal");
        vertMov = Input.GetAxis("Vertical");

        rb.velocity = transform.forward * speed * vertMov;
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

    void checkForHit()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, minDist))
        {
            if(hit.collider.tag == "Interactable")
            {
                print("SUPER");
                //Interactable interactable = hit.collider.GetComponent<Interactable>();
                interactableName.text = hit.collider.name;
            }
        } 
    }
}
