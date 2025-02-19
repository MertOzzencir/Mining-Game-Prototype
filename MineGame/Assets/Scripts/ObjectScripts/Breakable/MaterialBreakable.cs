using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MaterialBreakable : MonoBehaviour,IBreakable
{
    [SerializeField] private ParticleSystem Particle;
    [SerializeField] private float Health;
    [SerializeField] private float scaleRatio = 1.5f;
    [SerializeField] private float scaleTimer = 0.3f;

    BreakableType IBreakable.Type { get => ObjectType; set => ObjectType = value; }

    public BreakableType ObjectType;


    public void DestroyCondition(float Damage, Vector3 forceDirection)
    {
        Health -= Damage;
        if (Health >= 0) {
            ParticleAnimationHandler(forceDirection);
            transform.GetChild(transform.childCount-1).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(transform.GetChild(transform.childCount - 1).gameObject.GetComponent<SpawnItem>().DestroyObject());
            transform.GetChild(transform.childCount - 1).gameObject.transform.parent = null;
            HitAnimation();

        }
        if (transform.childCount == 0) {
            Destroy(gameObject,3f);
        }

    }

    public void HitAnimation()
    {
        StartCoroutine(AnimationTimer(gameObject));
    }
    public void ParticleAnimationHandler(Vector3 positionToInstantiate)
    {

        ParticleSystem sa = Instantiate(Particle, positionToInstantiate, Quaternion.identity);
    }
    
    IEnumerator AnimationTimer(GameObject scaleObject)
    {
        Vector3 localScale = scaleObject.transform.localScale;
        ObjectAnimatiorManager.Instance.ScaleObject(scaleObject.transform, new Vector3(scaleObject.transform.localScale.x / scaleRatio, scaleObject.transform.localScale.y / scaleRatio,
            scaleObject.transform.localScale.z / scaleRatio), scaleTimer);
        yield return new WaitForSeconds(scaleTimer);


    }


}
