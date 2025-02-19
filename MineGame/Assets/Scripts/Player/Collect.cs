using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Collect : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform CollectableTransform;

   
    private void OnEnable()
    {
        PlayerInput.OnMouseRightClick += CollectAction;

    }

    private void CollectAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, layer)) {

            ICollectable Collectable = hit.transform.GetComponent<ICollectable>();
            if (Collectable != null) {
                Collectable.Collect(CollectableTransform.position);
            }
        }
    }
    private void OnDisable()
    {
        PlayerInput.OnMouseRightClick -= CollectAction;
    }

}
