using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private KeyCode InventoryKey;
    [SerializeField] private GameObject InventoryBag;
    [SerializeField] private GameObject ItemBar;
    [SerializeField] private Transform InventoryOpenPoint;
    [SerializeField] private Transform InventoryClosePoint;
    [SerializeField] private Transform BarOpenPoint;
    [SerializeField] private Transform BarClosePoint;
    [SerializeField] private float OpenAndCloseTimer;
    [SerializeField] private GameObject FreeLookCamera;
    [SerializeField] private InventoryClickable CanSelectItem;
    [SerializeField] private Animator CharaterAnimation;
    private AnimationController PlayerAnimation;
    private PlayerMovement PlayerMovement;
    private Collect PlayerCollect;

    int state = 0;


    private void Start()
    {
        PlayerCollect = GetComponent<Collect>();
        PlayerAnimation = GetComponent<AnimationController>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

   

    void OpenBag()
    {
        CharaterAnimation.enabled = false;
        InventoryBag.SetActive(true);
        CanSelectItem.enabled = true;
        PlayerCollect.enabled = false;
        PlayerMovement.enabled = false;
        PlayerAnimation.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        FreeLookCamera.SetActive(false);
        InventoryBag.SetActive(true);
        ObjectAnimatiorManager.Instance.LocalMoveObject(InventoryBag.transform, InventoryOpenPoint.position, OpenAndCloseTimer);
        StartCoroutine(CloseBar());
        
    }
   
    IEnumerator CloseBag()
    {
        CharaterAnimation.enabled = true;
        CanSelectItem.enabled = false;
        PlayerCollect.enabled = true;
        PlayerMovement.enabled = true;
        PlayerAnimation.enabled = true;
        Cursor.lockState =CursorLockMode.Locked;
        FreeLookCamera.SetActive (true);
        ObjectAnimatiorManager.Instance.LocalMoveObject(InventoryBag.transform, InventoryClosePoint.position, OpenAndCloseTimer);
        yield return new WaitForSeconds(OpenAndCloseTimer);
        InventoryBag.SetActive(false);

    }

    IEnumerator OpenBar()
    {
        ItemBar.SetActive(true);
        ObjectAnimatiorManager.Instance.LocalMoveObject(ItemBar.transform, BarOpenPoint.position, OpenAndCloseTimer);
        yield return new WaitForSeconds(OpenAndCloseTimer);
        ItemBar.transform.position = BarOpenPoint.position;

    }

    IEnumerator CloseBar()
    {
        
        ObjectAnimatiorManager.Instance.LocalMoveObject(ItemBar.transform, BarClosePoint.position, OpenAndCloseTimer);
        yield return new WaitForSeconds(OpenAndCloseTimer);
        ItemBar.SetActive(false);
    }
    private void OnEnable()
    {
        PlayerInput.OnInventory += InventoryAction;
    }
    private void OnDisable()
    {
        PlayerInput.OnInventory -= InventoryAction;
    }
    private void InventoryAction()
    {
       
            state++;
            if (state == 1) {
                StartCoroutine(OpenBar());
            }
            else if (state == 2) {
                OpenBag();

            }
            else {
                state = 0;
                StartCoroutine(CloseBag());
                StartCoroutine(CloseBar());
            }
        
    }
}
