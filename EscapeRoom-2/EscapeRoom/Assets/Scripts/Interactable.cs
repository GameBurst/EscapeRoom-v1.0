using System;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public Sprite icon;

    public GameObject obj;

    public Camera camera;

    public float radius = 3f;
    public float minDist = 5f;

    //public static bool lookingAtThis;

    public static string objName;

    private void Start()
    {
        //lookingAtThis = false;
        objName = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && objName == transform.name)
        {
            checkIfTouched();
        }
    }

    private void checkIfTouched()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, minDist))
        {
            if(hit.collider.name == obj.name)
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

    public void spawn(Image image, Vector3 spawnCoords, Quaternion rotation)
    {
        Instantiate(obj);
        obj.transform.position = spawnCoords;
        print(obj.transform.position);
        obj.SetActive(true);
        image.sprite = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
