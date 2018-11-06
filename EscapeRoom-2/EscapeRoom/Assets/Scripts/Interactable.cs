using System;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public Sprite icon;

    public GameObject obj;

    public Camera camera;

    public float radius = 1f;
    public float minDist = 5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            checkIfTouched();
        }
    }

    private void checkIfTouched()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, minDist))
        {
            if(hit.collider.name == obj.name)
            {
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
