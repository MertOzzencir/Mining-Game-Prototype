using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour {
    [SerializeField] private LayerMask mask;
    [SerializeField] private float range;
    [SerializeField] private Animator currentAnimation;
    [SerializeField] private GameObject ParentOfWeapons;


    public GameObject currentWeapon;
   
    private void OnEnable()
    {
        PlayerInput.OnEquip += EquipAction;

    }

    private void EquipAction()
    {
        if (!currentAnimation.GetCurrentAnimatorStateInfo(0).IsName("PickaxeAnimation")) {
            if (currentWeapon == null) {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, range, mask)) {

                    IWeapon weapon = hit.transform.gameObject.GetComponent<IWeapon>();
                    if (weapon != null) {
                        currentWeapon = hit.transform.gameObject;
                        weapon.Equip(ParentOfWeapons);
                    }
                }
            }

            else {
                currentWeapon.GetComponent<IWeapon>().DeEquip();
                currentWeapon = null;
            }


        }
    }
    private void OnDisable()
    {
        PlayerInput.OnEquip -= EquipAction;
    }


}
