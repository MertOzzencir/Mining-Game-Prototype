using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlots : MonoBehaviour
{
    public static event Action<int> OnItemEnter;
    public int BARID;
    public InteractWithBar ItemBar;
    public LayerMask ItemMask;
    public GameObject SlotObject;
    Vector3 normalLocalScale;
  

  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ICollectable>() != null && SlotObject == null) {
            if(InventoryClickable.Instance.selectedObject != null) {
                ItemBar.ThreshHolderObject = InventoryClickable.Instance.selectedObject;
                SlotObject = InventoryClickable.Instance.selectedObject;
                normalLocalScale = SlotObject.transform.localScale;
                SlotObject.transform.parent = transform;
                SlotObject.transform.localScale *= 0.5f;
                SlotObject.transform.position = transform.position;
                InventoryClickable.Instance.selectedObject = null;
                OnItemEnter?.Invoke(BARID);

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ICollectable>() != null && SlotObject == other.gameObject) {
            if (InventoryClickable.Instance.selectedObject != null) {
                Destroy(ItemBar.ICONObject);
                SlotObject.transform.parent = InventoryClickable.Instance.gameObject.transform;
                SlotObject.transform.localScale = normalLocalScale;
                SlotObject = null;
                
            }
                
        }
    }



    
}
