using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public float radius = 3f;

    public Sprite icon;

    public GameObject obj;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void take(Image image)
    {
        image.sprite = icon;
        obj.SetActive(false);
        print("Am setat");
    }

    public void spawn(Image image, Vector3 spawnCoords, Quaternion rotation)
    {
        Instantiate(obj, spawnCoords, rotation);
        obj.SetActive(true);
        image.sprite = null;
    }
}
