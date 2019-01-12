using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenByHand : MonoBehaviour {

    public GameObject obj;
    public Camera camera;
    public Animator animator;

    public string objName;
    float minDist = 6f;
    public bool pointingAtThis;

    // Use this for initialization
    void Start () {
        animator.enabled = false;
        pointingAtThis = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (pointingAtThis && Input.GetMouseButtonDown(0))
        {
            checkIfTouched();
        }
    }

    private void checkIfTouched()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, minDist))
        {
            //print(Input.mousePosition);
            if (hit.collider.tag == "OpenByHand")
            {
                print("ok");
                if(hit.collider.GetComponent<OpenByHand>().objName == objName)
                {
                    print("Super");
                    obj.tag = "Untagged";
                    animator.enabled = true;
                    pointingAtThis = false;
                    return;
                }
            }
        }
    }
}
