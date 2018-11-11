using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public Camera camera;

    protected Joystick joystick;
    protected int IntialTouchesPosition;

    public Text interactableName;

    public Image[] inventorySlots;

    public Interactable[] intObjects;

    Interactable interactable;

    public Vector3 spawnPosition;
    private Vector3 noPos;

    private Touch iTouch;

    public float minDist;
    public float speed;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float horizMov, vertMov;
    private float lim = 10f;

    private string hitName;

    private bool canPlace, directionChosen;
    public static bool plsTake;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType <Joystick>();

        canPlace = false;
        noPos = new Vector3(0f, 0f, 0f);
        spawnPosition = noPos;
        intObjects = new Interactable[7];
        plsTake = false;
	}
	
	// Update is called once per frame
	void Update () {
        playerMove();
        cameraMove();
        checkForHit();
        tempTake();
        //tempSpawn();
    }

    void playerMove()
    {
        
        rb.velocity = joystick.Vertical * speed * transform.forward + joystick.Horizontal * speed * transform.right;

        if(rb.velocity.x == 0 && rb.velocity.y == 0) // acest if si functia subordonata lui sunt pc only(testing)
        {
            rb.velocity = (transform.forward * speed * Input.GetAxis("Vertical")) + (transform.right * speed * Input.GetAxis("Horizontal"));
        }

    }

    void cameraMove()
    {
        for(int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && 
                Input.GetTouch(i).position.x < 300 && Input.GetTouch(i).position.y < 300)
            {
                IntialTouchesPosition = i;
            }
        }

        if (((joystick.Horizontal == 0.0f) || (joystick.Vertical == 0.0f)) && Input.touchCount == 1)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    // Record initial touch position.
                    case TouchPhase.Began:
                        iTouch = touch;
                        break;

                    // Determine direction by comparing the current touch position with the initial one.
                    case TouchPhase.Moved:
                        float xMoveDist = speedH * (iTouch.position.x - touch.position.x), yMoveDist = speedV * (iTouch.position.y - touch.position.y);
                        yaw -= xMoveDist;

                        if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
                            pitch += yMoveDist;

                        camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
                        rb.transform.eulerAngles = new Vector3(0f, yaw, 0f);
                        iTouch = touch;
                        break;

                    // Report that a direction has been chosen when the finger is lifted.
                    case TouchPhase.Ended:
                        iTouch = new Touch();
                        break;
                }
            }
        }
        else if (((joystick.Horizontal != 0.0f) || (joystick.Vertical != 0.0f)) && Input.touchCount > 1)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Touch touch = Input.GetTouch(i);
                if (IntialTouchesPosition != i)
                {
                    switch (touch.phase)
                    {
                        // Record initial touch position.
                        case TouchPhase.Began:
                            iTouch = touch;
                            break;

                        // Determine direction by comparing the current touch position with the initial one.
                        case TouchPhase.Moved:
                            float xMoveDist = speedH * (iTouch.position.x - touch.position.x), yMoveDist = speedV * (iTouch.position.y - touch.position.y);
                            yaw -= xMoveDist;

                            if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
                                pitch += yMoveDist;

                            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
                            rb.transform.eulerAngles = new Vector3(0f, yaw, 0f);
                            iTouch = touch;
                            break;

                        // Report that a direction has been chosen when the finger is lifted.
                        case TouchPhase.Ended:
                            iTouch = new Touch();
                            break;
                    }
                }
            }
        }

        //Conditii pentru android - > camera sa se miste 

        
        
        if (Input.GetButton("Fire1") && ((joystick.Horizontal == 0.0f) || (joystick.Vertical == 0.0f))) //Pentru Pc(debugging)        
        {
            float xMoveDist = speedH * Input.GetAxis("Mouse X"), yMoveDist = speedV * Input.GetAxis("Mouse Y");

            yaw -= xMoveDist;

            if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
                pitch += yMoveDist;

            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            rb.transform.eulerAngles = new Vector3(0f, yaw, 0f);
        } // Conditie pt miscare pe PC
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
                Interactable.objName = interactable.name;
                interactableName.text = hit.collider.name;
            } else Interactable.objName = null;

            interactableName.text = hit.collider.name;
            canPlace = true;
            hitName = hit.collider.name;
            spawnPosition = hit.point;
        }
        else
        {
            hitName = null;
            interactable = null;
            interactableName.text = " ";
            canPlace = false;
            spawnPosition = noPos;
        }
    }

    public void tempTake()
    {
        if(interactable != null && (Input.GetKeyDown(KeyCode.E) || plsTake))
        {
            for(int i = 0; i < intObjects.Length; ++i)
            {
                if(intObjects[i] == null)
                {
                    intObjects[i] = interactable;
                    interactable.take(inventorySlots[i]);
                    plsTake = false;
                    return;
                }
            }
            plsTake = false;
        }
        plsTake = false;
    }

    public void removeObject(int index)
    {
        if(intObjects[index] != null && canPlace)
        {
            intObjects[index].spawn(inventorySlots[index], spawnPosition, transform.rotation, hitName);
            intObjects[index] = null;
            return;
        }
    }
}
