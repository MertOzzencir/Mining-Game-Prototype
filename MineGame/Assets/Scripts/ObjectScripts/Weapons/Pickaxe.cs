using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour, IWeapon
{
    public WeaponType WeaponType => WeaponType.PickAxe;


    BoxCollider BC;
    bool equiped;

    Vector3 SetPosition = new Vector3(0.180000007f, 0.324999988f, -0.26699999f);
    Vector3 SetRotation = new Vector3(351.25473f, 249.79361f, 256.681396f);

   
    public int LevelOfWeapon { get => Level; set => Level=value; }
    public int Level;
    private void Start()
    {
        BC = GetComponent<BoxCollider>();
        
    }
    private void Update()
    {
        if (!equiped) {

            transform.Rotate(Vector3.up * 50f*Time.deltaTime);

            }
    }
    public void Equip(GameObject Parent)
    {
        equiped = true;
        BC.enabled = false;
        transform.parent = Parent.transform;
        StartCoroutine(SetPositionAnimation());
        StartCoroutine(SetRotationAnimation());


    }

    public void DeEquip()
    {
        transform.eulerAngles = Vector3.zero;
        equiped = false;
        transform.parent = null;
        BC.enabled = true;
    }

    IEnumerator SetPositionAnimation()
    {
        while (Vector3.Distance(transform.localPosition ,SetPosition)>0.01f) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, SetPosition, 0.2f);
            yield return null;

        }

    }
    IEnumerator SetRotationAnimation()
    {
        Quaternion targetRotation = Quaternion.Euler(SetRotation); 

        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.01f) 
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 500f * Time.deltaTime);
            yield return null; 
        }

        transform.localRotation = targetRotation;
    }


}
