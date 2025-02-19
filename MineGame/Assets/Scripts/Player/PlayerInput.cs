using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector2> OnWASD; 
    public static event Action OnMouseLeftClick;
    public static event Action OnMouseRightClick;
    public static event Action OnEquip;
    public static event Action OnInventory;
    public static event Action<bool> OnRun;

    PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }
  

    private void Equip_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEquip?.Invoke();
    }

    private void MouseRightClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMouseRightClick?.Invoke();
    }

    private void MouseLeftClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMouseLeftClick?.Invoke();
    }

    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode SpeedKey = KeyCode.LeftShift;
    private void Update()
    {
        GetPlayerInputs();
    }
    public void GetPlayerInputs()
    {
        PlayerMovement.instance.InputDirection = playerInputAction.Player.Move.ReadValue<Vector2>();
        Vector2 input = playerInputAction.Player.Move.ReadValue<Vector2>();
        OnWASD?.Invoke(input);
        

    }

  
    private void OnEnable()
    {
        playerInputAction.Player.Inventory.performed += Inventory_performed;
        playerInputAction.Player.MouseLeftClick.performed += MouseLeftClick_performed;
        playerInputAction.Player.MouseRightClick.performed += MouseRightClick_performed;
        playerInputAction.Player.Equip.performed += Equip_performed;
        playerInputAction.Player.Run.started += RunON;
        playerInputAction.Player.Run.canceled += RuNOFF; ;

    }

    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInventory?.Invoke();
    }

    private void RuNOFF(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRun?.Invoke(false);
    }

    private void RunON(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRun?.Invoke(true); 
    }

    private void OnDisable()
    {
        playerInputAction.Player.MouseLeftClick.performed -= MouseLeftClick_performed;
        playerInputAction.Player.MouseRightClick.performed -= MouseRightClick_performed;
        playerInputAction.Player.Equip.performed -= Equip_performed;
    }

}
