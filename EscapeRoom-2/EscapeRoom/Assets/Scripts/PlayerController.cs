using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public Camera camera;

    public Joystick moveJoystick;
    public Joystick cameraJoystick;
    protected int IntialTouchesPosition;

    public Text interactableName, inSlotObjName;

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
    private Elevator elevator;
    private GameObject lockableObj;

    public Button interactButton;

    //debugging
    public static bool ghostMode;
    public GameObject capsule;
    public Toggle ghMToggle;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        //moveJoystick = FindObjectOfType <Joystick>();

        canPlace = false;
        intObjects = new Interactable[7];
        plsTake = false;
        isPaused = false;

        interactButton.gameObject.SetActive(false);

        inSlotObjName.text = "";

        ///debug
        ghostMode = false;
        ghMToggle.isOn = false;
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
        if (!ghostMode)
        {
            Vector3 moveDirection = moveJoystick.Vertical * speed * transform.forward + moveJoystick.Horizontal * speed * transform.right;
            moveDirection.y = rb.velocity.y;
            rb.velocity = moveDirection;
        }
        else
        {
            Vector3 moveDirection = moveJoystick.Vertical * speed * 3 * transform.forward + moveJoystick.Horizontal * speed * 3 * transform.right;
            rb.velocity = moveDirection;
        }

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

        if(!ghostMode)
            rb.transform.eulerAngles = new Vector3(0f, yaw, 0f);
        else rb.transform.eulerAngles = new Vector3(pitch, yaw, rb.transform.rotation.eulerAngles.z);

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
            if(hit.collider.tag != "Untagged" && hit.collider.tag != "Lockable")
            {
                theObj = hit.collider.GetComponent<InterObjjj>();
                interactButton.gameObject.SetActive(true);
            } else
            {
                theObj = null;
                interactButton.gameObject.SetActive(false);

                if (hit.collider.tag == "Lockable")
                    lockableObj = hit.collider.gameObject;
                else lockableObj = null;

                if(hit.collider.tag == "Elevator")
                {
                    elevator = hit.collider.GetComponent<Elevator>();
                }
            }

            if(hit.collider.tag != "Untagged")
            {
                if(hit.collider.tag == "Lockable")
                    interactableName.text = hit.collider.name + " (Locked)";
                else if(hit.collider.tag == "Elevator")
                    interactableName.text = hit.collider.name + Elevator.state;
                else interactableName.text = hit.collider.name;
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
            interactableName.text = "";
            canPlace = false;
        }
    }

    public void removeObject(int index)
    {
        //print(canPlace);
        //print(intObjects[index]);
        if (intObjects[index] != null && canPlace)
        {
            print("Index: " + index);
            print("Sprite: " + inventorySlots[index]);
            print("Lockable obj: " + lockableObj);
            if (intObjects[index].spawn(inventorySlots[index], lockableObj))
            {
                intObjects[index] = null;
            }

            return;
        }
    }

    public void startAction()
    {
        print(theObj);
        theObj.Activate();
    }

    public void onButtonPress(int i)
    {
        if (intObjects[i] == null || interactableName.text != "")
            return;

        inSlotObjName.text = intObjects[i].name;
    }

    public void onButtonRelease(int i)
    {
        if (intObjects[i] == null)
            return;

        inSlotObjName.text = "";
    }

    ///debugging
    public void enableGhostMode(bool ioi)
    {
        ghostMode = ghMToggle.isOn;
        print(ghostMode);

        if (ghostMode == true)
        {
            capsule.GetComponent<Collider>().enabled = false;
            rb.useGravity = false;
        }
        else
        {
            capsule.GetComponent<Collider>().enabled = true;
            rb.useGravity = true;
            rb.rotation = Quaternion.identity;
        }

    }
}
