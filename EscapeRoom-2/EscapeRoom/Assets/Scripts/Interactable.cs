using System;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : InterObjjj
{
    public Sprite icon;

    public GameObject obj;
    private Interactable inter;

    public GameObject target, missingTargetPiece;
    public Animator targetAnimator;

    public Camera camera;

    public float radius = 3f;
    float minDist = 6f;

    public static string objName;


    public Image[] inventorySlots;

    private void Start()
    {
        inter = gameObject.GetComponent<Interactable>();
        if(missingTargetPiece != null)
        {
            missingTargetPiece.SetActive(false);
            targetAnimator.enabled = false;
        }
        
        //objName = null;
    }

    public void take(Image image)
    {
        image.sprite = icon;
        obj.SetActive(false);
        print("Am setat");
    }

    public bool spawn(Image image, GameObject obj)
    {
        //print(hitName);
        //print("***" + target.name);
        //print("*****" + obj.name);
        if (obj == target)
        {
            if(missingTargetPiece != null)
            {
                missingTargetPiece.SetActive(true);
                targetAnimator.enabled = true;
            }
            
            target.tag = "Untagged";
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

    public override void Activate()
    {
        inventorySlots = new Image[7];
        inventorySlots[0] = GameObject.Find("/HUD/UICanvas/Inventory/InventorySlot/ItemButton/Icon").GetComponent<Image>();
        inventorySlots[1] = GameObject.Find("/HUD/UICanvas/Inventory/InventorySlot (1)/ItemButton/Icon").GetComponent<Image>();
        inventorySlots[2] = GameObject.Find("/HUD/UICanvas/Inventory/InventorySlot (2)/ItemButton/Icon").GetComponent<Image>();
        inventorySlots[3] = GameObject.Find("/HUD/UICanvas/Inventory/InventorySlot (3)/ItemButton/Icon").GetComponent<Image>();
        inventorySlots[4] = GameObject.Find("/HUD/UICanvas/Inventory/InventorySlot (4)/ItemButton/Icon").GetComponent<Image>();
        inventorySlots[5] = GameObject.Find("/HUD/UICanvas/Inventory/InventorySlot (5)/ItemButton/Icon").GetComponent<Image>();
        inventorySlots[6] = GameObject.Find("/HUD/UICanvas/Inventory/InventorySlot (6)/ItemButton/Icon").GetComponent<Image>();

        for (int i = 0; i < PlayerController.intObjects.Length; ++i)
        {
            if (PlayerController.intObjects[i] == null)
            {
                PlayerController.intObjects[i] = inter;
                print(PlayerController.intObjects[i]);
                inventorySlots[i].sprite = icon;
                obj.SetActive(false);
                print("Am setat");

                return;
            }
        }
    }
}
