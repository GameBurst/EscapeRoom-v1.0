using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public Camera camera;

    public Joystick moveJoystick;
    public Joystick cameraJoystick;
    protected int IntialTouchesPosition;

    public Text interactableName;

    public Image[] inventorySlots;

    public static Interactable[] intObjects;

    Interactable interactable;
    OpenByHand obh;

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
    public static bool plsTake, isPaused;

    private InterObjjj theObj;
    private GameObject lockableObj;

    public Button interactButton;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        //moveJoystick = FindObjectOfType <Joystick>();

        canPlace = false;
        intObjects = new Interactable[7];
        plsTake = false;
        isPaused = false;

        interactButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            playerMove();
            cameraMove();
            checkForHit();
        }
    }

    void playerMove()
    {
        Vector3 moveDirection = moveJoystick.Vertical * speed * transform.forward + moveJoystick.Horizontal * speed * transform.right;
        moveDirection.y = rb.velocity.y;
        rb.velocity = moveDirection;

        /*if(rb.velocity.x == 0 && rb.velocity.y == 0) // acest if si functia subordonata lui sunt pc only(testing)
        {
            rb.velocity = (transform.forward * speed * Input.GetAxis("Vertical")) + (transform.right * speed * Input.GetAxis("Horizontal"));
        }
        */
    }

    void cameraMove()
    {
        ///////// miscare noua blanao

        float xMoveDist = -speedH * cameraJoystick.Horizontal, yMoveDist = -speedV * cameraJoystick.Vertical;
        yaw -= xMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

        if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
            pitch += yMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

        camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        rb.transform.eulerAngles = new Vector3(0f, yaw, 0f);

        ///////
        /*

        for (int i = 0; i < Input.touchCount; ++i)
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
                        yaw -= xMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

                        if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
                            pitch += yMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

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
                            yaw -= xMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

                            if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
                                pitch += yMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

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

        */
        /*
        if (Input.GetButton("Fire1") && ((joystick.Horizontal == 0.0f) || (joystick.Vertical == 0.0f))) //Pentru Pc(debugging)        
        {
            float xMoveDist = speedH * Input.GetAxis("Mouse X"), yMoveDist = speedV * Input.GetAxis("Mouse Y");

            yaw -= xMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

            if (-90 <= pitch + yMoveDist && pitch + yMoveDist <= 90)
                pitch += yMoveDist * PlayerPrefs.GetFloat("CameraSensibility");

            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            rb.transform.eulerAngles = new Vector3(0f, yaw, 0f);
        }*/  // Conditie pt miscare pe PC
    }

    void checkForHit()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, minDist))
        {
            //print(hit.collider.name);
            //print(hit.collider.tag);

            if(hit.collider.tag != "Untagged" && hit.collider.tag != "Lockable")
            {
                theObj = hit.collider.GetComponent<InterObjjj>();
                //print("OBIECTTTTTTTTTTT" + theObj);
                //print(theObj);
                interactButton.gameObject.SetActive(true);
            } else
            {
                if (hit.collider.tag == "Lockable")
                    lockableObj = hit.collider.gameObject;
                else lockableObj = null;
                theObj = null;
                interactButton.gameObject.SetActive(false);
            }

            /*********

            if (hit.collider.tag == "Interactable")
            {
                //zp = hit.collider.GetComponent<InteractionContainer>().Target;
                //interactButton.gameObject.SetActive(true);

                //print("SUPER");
                interactable = hit.collider.GetComponent<Interactable>();
                Interactable.objName = interactable.name;
                interactableName.text = hit.collider.name;
            }
            else
            {
                //zp = hit.collider.GetComponent<InteractionContainer>().Target;
                //interactButton.gameObject.SetActive(true);

                Interactable.objName = null;
                if (hit.collider.tag == "Lockable")
                {
                    interactableName.text = hit.collider.name + " (Locked)";
                    if(hit.collider.name == "Safe-Deposit Box Door")
                    {
                        if (Seif.notEnteringCode)
                        {
                            Seif.pointingAtThis = true;
                            //print("ma uit la cufar");
                        } 
                    }
                    else
                    {
                        Seif.pointingAtThis = false;
                        //print("nu ma uit la cufar");
                    }
                }
                else if(hit.collider.tag == "OpenByHand")
                {
                    obh = hit.collider.GetComponent<OpenByHand>();
                    interactableName.text = obh.objName;
                    obh.pointingAtThis = true;
                }
                else
                {
                    interactableName.text = null;
                }
            }

            //interactableName.text = hit.collider.name;

            *************/

            if(hit.collider.tag != "Untagged")
            {
                if(hit.collider.tag != "Lockable")
                    interactableName.text = hit.collider.name;
                else interactableName.text = hit.collider.name + " (Locked)";
            } else
            {
                interactableName.text = "";
            }

            canPlace = true;
            if(hit.collider.tag == "Lockable")
                hitName = hit.collider.name;
        }
        else
        {
            interactButton.gameObject.SetActive(false);

            lockableObj = null;
            //hitName = null;
            //interactable = null;
            interactableName.text = "";
            canPlace = false;
            //Seif.pointingAtThis = false;
        }
    }

    public void removeObject(int index)
    {
        print(canPlace);
        print(intObjects[index]);
        if (intObjects[index] != null && canPlace)
        {
            if (intObjects[index].spawn(inventorySlots[index], lockableObj))
            {
                intObjects[index] = null;
            }

            return;
        }
    }

    public void startAction()
    {
        //zp.Interact();
        print(theObj);
        theObj.Activate();
    }
}
