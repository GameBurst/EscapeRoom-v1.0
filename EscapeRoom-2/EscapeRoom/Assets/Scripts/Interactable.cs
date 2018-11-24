using System;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public Sprite icon;

    public GameObject obj;

    public GameObject target;
    public Animator targetAnimator;

    public Camera camera;

    public float radius = 3f;
    public float minDist = 5f;

    public static string objName;

    private void Start()
    {
        targetAnimator.enabled = false;
        objName = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetAnimator.enabled = true;
        }

        if (Input.GetMouseButtonDown(0) && objName == transform.name)
        {
            print("Touching Slot");
            checkIfTouched();
        }
    }

    private void checkIfTouched()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, minDist))
        {
            print(hit.collider.name);
            print(obj.name);
            if (hit.collider.name == obj.name)
            {
                print("o da");
                PlayerController.plsTake = true;
                return;
            }
        }
    }

    public void take(Image image)
    {
        image.sprite = icon;
        obj.SetActive(false);
        print("Am setat");
    }

    public bool spawn(Image image, Vector3 spawnCoords, Quaternion rotation, string hitName)
    {
        if (hitName == target.name)
        {
            //Instantiate(obj);
            targetAnimator.enabled = true;
            target.tag = "Untagged";
            //obj.transform.position = spawnCoords;
            //print(obj.transform.position);
            //obj.SetActive(true);
            image.sprite = null;

            return true;
        }
        else return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
