using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClickable : MonoBehaviour
{
    public static InventoryClickable Instance;  
    public LayerMask groundLayer;
    public LayerMask ObjectLayer;// Raycast'in sadece zemini hedef almas� i�in
    public GameObject selectedObject;  // Se�ilen obje
    private Camera mainCamera;

    void Start()
    {
        Instance = this;
        mainCamera = Camera.main; // Ana kameray� al�yoruz
    }

    void Update()
    {
        // Sol t�k ile obje se�me
        if (Input.GetMouseButtonDown(0)) {
            SelectObject();
        }

        // Obje se�iliyse, fareyi takip ettir
        if (selectedObject != null) {
            MoveObjectToMouse();
        }

        // Sa� t�k ile se�imi iptal et
        if (Input.GetMouseButtonDown(1)) {
            selectedObject = null;
        }
    }

    void SelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity,ObjectLayer)) {
           
                selectedObject = hit.collider.gameObject;
            
        }
    }

    void MoveObjectToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer)) {
            // E�er zemin ile �arp��ma varsa
            Vector3 targetPosition = hit.point;
            selectedObject.transform.position = targetPosition;
        }
    }
}
