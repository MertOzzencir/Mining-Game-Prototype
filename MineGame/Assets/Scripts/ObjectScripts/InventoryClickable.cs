using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClickable : MonoBehaviour
{
    public static InventoryClickable Instance;  
    public LayerMask groundLayer;
    public LayerMask ObjectLayer;// Raycast'in sadece zemini hedef almasý için
    public GameObject selectedObject;  // Seçilen obje
    private Camera mainCamera;

    void Start()
    {
        Instance = this;
        mainCamera = Camera.main; // Ana kamerayý alýyoruz
    }

    void Update()
    {
        // Sol týk ile obje seçme
        if (Input.GetMouseButtonDown(0)) {
            SelectObject();
        }

        // Obje seçiliyse, fareyi takip ettir
        if (selectedObject != null) {
            MoveObjectToMouse();
        }

        // Sað týk ile seçimi iptal et
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
            // Eðer zemin ile çarpýþma varsa
            Vector3 targetPosition = hit.point;
            selectedObject.transform.position = targetPosition;
        }
    }
}
