using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;

    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private KeyCode MoveForwardKey;
    [SerializeField] private KeyCode Left;
    [SerializeField] private KeyCode Backward;
    [SerializeField] private KeyCode Right;
    [SerializeField] private KeyCode Run;

    private void Start()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        PlayerInput.OnWASD += AnimationControllerHandle;

    }
    private void OnDisable()
    {
        PlayerInput.OnWASD -= AnimationControllerHandle;
    }

    private void AnimationControllerHandle(Vector2 input)
    {
        if (input == Vector2.zero) {
            ResetAnimation();
            return;

        }

        if (input.x == 1 && input.x == -1)
            ResetAnimation();
        else if (input.y == 1 && input.y == -1)
            ResetAnimation();
        else if (Input.GetKey(Run) && input.y == 1) {
            SetPlayerMovement(true, false, false, false, true);
        }
        else if (input.y == 1 && input.x == 1)
            SetPlayerMovement(true, false, false, false, false);
        else if (input.y == 1 && input.x == -1)
            SetPlayerMovement(true, false, false, false, false);
        else if (input.y == 1 )
            SetPlayerMovement(true, false, false, false, false);
        else if (input.y == -1)
            SetPlayerMovement(false, false, false, true, false);
        else if (input.y == -1 && input.x == 1)
            SetPlayerMovement(false, false, false, true, false);
        else if (input.y == -1 && input.x == -1)
            SetPlayerMovement(false, false, false, true, false);
        else if (input.x == 1)
            SetPlayerMovement(false, true, false, false, false);
        else if (input.x == -1)
            SetPlayerMovement(false, false, true, false, false);
       
    }

 
    public void SetPlayerMovement(bool moveForward,bool moveRight,bool moveLeft,bool canBackWard,bool canRun)
    {
        PlayerAnimator.SetBool("canWalk", moveForward);
        PlayerAnimator.SetBool("canRight", moveRight);
        PlayerAnimator.SetBool("canLeft", moveLeft);
        PlayerAnimator.SetBool("canBackWard", canBackWard);
        PlayerAnimator.SetBool("canRun", canRun);

    }
    public void ResetAnimation()
    {
        SetPlayerMovement(false, false, false, false, false);

    }

    public void CanMine()
    {
        ResetAnimation();
        PlayerAnimator.SetTrigger("canMine");

    }


}
