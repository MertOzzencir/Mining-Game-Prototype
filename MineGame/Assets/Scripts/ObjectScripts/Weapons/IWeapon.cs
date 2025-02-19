
using UnityEngine;


public enum WeaponType {

    Axe,
    PickAxe
}
public interface IWeapon 
{
    public WeaponType WeaponType { get;}
    
    public int LevelOfWeapon {  get; set; }

    void Equip(GameObject ParentOfWeapon);

    void DeEquip();


   
}
