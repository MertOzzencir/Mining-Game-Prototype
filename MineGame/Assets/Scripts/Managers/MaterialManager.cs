using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager instance;
    public BoxCollider Bag;

    float zMax;
    float zMin;
    float xMax;
    float xMin;
    Bounds BagBounds;



    public static int Silver;
    public static int Wood;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        BagBounds = Bag.bounds;
        zMax= BagBounds.max.z;
        zMin = BagBounds.min.z; 
        xMax = BagBounds.max.x;
        xMin = BagBounds.min.x;

    }
    public void AddToInventory(MaterialType material,GameObject Item)
    {
        if (material == MaterialType.Wood) {
            Wood++;

        }
           
        if(material == MaterialType.Silver) {
            Silver++;

        }
        AddItemOnInventory(Item);

    }

    void AddItemOnInventory(GameObject Item)
    {
        Bag.gameObject.SetActive(true);
        Item.transform.localRotation = Quaternion.Euler(0, 0, 0);
        float z = Random.Range(-2, 2);
        float x = Random.Range(-2, 2);
        Vector3 random=new Vector3(x,0,z);
        Vector3 position = new Vector3(BagBounds.center.x, BagBounds.center.y, BagBounds.center.z);
        Vector3 center = Bag.bounds.center;
        Vector3 targetPoint = new Vector3(0, -1, 0); // Hedef bir nokta (örnek)
        Vector3 direction = (targetPoint - center).normalized; // Yön vektörü (normalize edilmiþ)
        GameObject newItem = Instantiate(Item, Bag.transform.TransformPoint(Bag.center + random) ,Quaternion.identity);
        newItem.transform.parent = Bag.transform;
        newItem.transform.localRotation = Quaternion.Euler(21.4908009f, 85.1239395f, 166.476868f);
        newItem.transform.localScale /= 5;
        newItem.GetComponent<Rigidbody>().isKinematic = true;
        newItem.GetComponent<Collectable>().enabled = false;
        Bag.gameObject.SetActive(false);

    }
}
