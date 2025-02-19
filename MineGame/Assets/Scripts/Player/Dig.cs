using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Dig : MonoBehaviour
{
    public float range;
    public float digDamage;
    public float clickCoolDown = 3f;


    float timer;

    IBreakable BreakableObject;
    EquipWeapon Equipweapon;
    Vector3 ParticlePosition;
    [SerializeField] private LayerMask layer;

    private void Start()
    {
        Equipweapon = GetComponent<EquipWeapon>();
        PlayerInput.OnMouseLeftClick += DigAction;
    }

    private void DigAction()
    {
        
        if (timer > clickCoolDown && Equipweapon.currentWeapon != null) {
            timer = 0;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range, layer)) {
                BreakableObject = hit.transform.gameObject.GetComponent<IBreakable>();
                if (BreakableObject != null && Equipweapon.currentWeapon.GetComponent<IWeapon>().WeaponType == WeaponType.PickAxe && BreakableObject.Type == BreakableType.Stone) {
                    ParticlePosition = hit.point;
                    AnimationEventScript.instance.SetValues(BreakableObject, ParticlePosition, digDamage);
                    AnimationController.Instance.CanMine();
                }

                if (BreakableObject != null && Equipweapon.currentWeapon.GetComponent<IWeapon>().WeaponType == WeaponType.Axe && BreakableObject.Type == BreakableType.Tree) {
                    ParticlePosition = hit.point;

                    AnimationEventScript.instance.SetValues(BreakableObject, ParticlePosition, digDamage);
                    AnimationController.Instance.CanMine();


                }
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnDisable()
    {
        PlayerInput.OnMouseLeftClick -= DigAction;
    }
}
