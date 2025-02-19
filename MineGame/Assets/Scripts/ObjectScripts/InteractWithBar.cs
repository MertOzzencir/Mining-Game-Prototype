using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class InteractWithBar : MonoBehaviour
{
    public GameObject ThreshHolderObject;
    public GameObject ICONObject;
    public KeyCode UseKeyCode;
    public LayerMask Mask;
    public int BARID;

    bool canOnGround;
    private void Start()
    {
        ItemSlots.OnItemEnter += ItemSlots_OnItemEnter;
    }

    private void Update()
    {
        if (Input.GetKeyDown(UseKeyCode)) {
            canOnGround = true;
        }

        if (canOnGround) {
            if (ICONObject != null) {
                if (ICONObject.GetComponent<Collectable>() != null) {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit,15f,Mask)) {
                        ICONObject.transform.parent = null;
                        ICONObject.transform.position = new Vector3(hit.point.x,.5f,hit.point.z);
                        ICONObject.GetComponent<Rigidbody>().isKinematic = false;
                        ICONObject.transform.localScale = Vector3.one *15f;
                        ICONObject.GetComponent<Collectable>().enabled = true;
                        Destroy(ThreshHolderObject);
                        ICONObject = null;
                        canOnGround = false;
                        
                    }
                }
            }
        }
    }


    private void ItemSlots_OnItemEnter(int SlotID)
    {
        if(BARID == SlotID ) {
            ICONObject = Instantiate(ThreshHolderObject,transform.position,Quaternion.identity);
            ICONObject.transform.parent = transform;
            ICONObject.transform.localPosition = Vector3.zero;
            ICONObject.transform.localRotation = Quaternion.Euler(21.4908009f, 85.1239395f, 166.476868f);

            ICONObject.transform.localScale *= 5f;

        }
    }
}
