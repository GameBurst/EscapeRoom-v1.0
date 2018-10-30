using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public Camera camera;

    protected Joystick joystick;

    public Text interactableName;

    public Image[] inventorySlots;

    Interactable interactable;

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
        joystick = FindObjectOfType <Joystick>();
	}
	
	// Update is called once per frame
	void Update () {
        playerMove();
        cameraMove();
        checkForHit();
        tempSpawn();
    }

    void playerMove()
    {
<<<<<<< HEAD
        /*rb.velocity = new Vector3((joystick.Horizontal * speed) + (Input.GetAxis("Horizontal") * speed),
                                    rb.velocity.y, 
                                   (joystick.Vertical * speed) + (Input.GetAxis("Vertical") * speed));*/ //pt telefon e buna asta, dar daca rotesti playerul la 180 de grade spre ex si apesi w se duce cu spatele
        rb.velocity = transform.forward * speed * Input.GetAxis("Vertical");
=======

        rb.velocity = new Vector3((joystick.Horizontal * speed) + (Input.GetAxis("Horizontal") * speed),
                                  rb.velocity.y, 
                                  (joystick.Vertical * speed) + (Input.GetAxis("Vertical") * speed));
>>>>>>> 96fe34625cda0bd51f1b696efd06a5a2e610af82
    }

    void cameraMove()
    {
<<<<<<< HEAD
        if (Input.GetButton("Fire1"))// && (Input.touchCount > 1))// <-Pentru Android se decomenteaza si se introduce sub acelasi if
=======
        if (Input.GetButton("Fire1")) //&& (Input.touchCount > 1)) <-Pentru Android se decomenteaza si se introduce sub acelasi if
>>>>>>> 96fe34625cda0bd51f1b696efd06a5a2e610af82
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

        if (Physics.Raycast(ray, out hit, minDist))
        {
            if (hit.collider.tag == "Interactable")
            {
                //print("SUPER");
                interactable = hit.collider.GetComponent<Interactable>();
                interactable.setTexture(inventorySlots[0]);
                interactable.popOut();
                interactableName.text = hit.collider.name;
            }
        }
        else
        {
            interactableName.text = " ";
        }
    }

    void tempSpawn()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(interactable != null)
            {
                interactable.spawn(inventorySlots[0], transform.position, transform.rotation);
            }
        }
    }
}
