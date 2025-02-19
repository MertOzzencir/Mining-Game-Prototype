using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] float DestroyTransformTimer;
    [SerializeField] GameObject BountyToSpawn;
    Rigidbody rb;
    MeshCollider mC;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mC = GetComponent<MeshCollider>();
    }
    public IEnumerator DestroyObject()
    {

        yield return new WaitForSeconds(.5f);
        ObjectAnimatiorManager.Instance.ScaleObject(transform, Vector3.zero, DestroyTransformTimer);
        yield return new WaitForSeconds(DestroyTransformTimer);

        Instantiate(BountyToSpawn, transform.position + new Vector3(0, transform.localScale.y +1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        mC.enabled = false;

        rb.isKinematic = true;

        Destroy(gameObject);
    }
}
