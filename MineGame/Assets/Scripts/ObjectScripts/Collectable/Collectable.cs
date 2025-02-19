using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable {
    Bag bag;
    Rigidbody rb;
    BoxCollider BC;
    Animator animator;

    public MaterialType Type { get => type; set => type = value; }

    public MaterialType type;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        BC = GetComponent<BoxCollider>();
    }

    private void Awake()
    {
        bag = FindAnyObjectByType<Bag>();
        animator = bag.GetComponent<Animator>();
    }
    public void Collect(Vector3 positionToCollect)
    {
        animator.SetBool("canOpen",true);
        MaterialManager.instance.AddToInventory(Type,gameObject);
        rb.isKinematic = true;
        BC.enabled = false;
        StartCoroutine(MoveTowardsToBag(positionToCollect));
        HandleAnimation();
    }

    public void HandleAnimation()
    {
        ObjectAnimatiorManager.Instance.ScaleObject(transform, Vector3.zero, 1f);
    }

    IEnumerator MoveTowardsToBag(Vector3 FinalPosition)
    {

        while (Vector3.Distance(transform.position, FinalPosition) > .1f) {

            transform.position = Vector3.MoveTowards(transform.position, FinalPosition, 0.1f);
            yield return new WaitForSeconds(0.01f);  
        }
        yield return null;
        animator.SetBool("canOpen", false);

        Destroy(gameObject, 1f);

    }

    
}
