using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] private GameObject movementParticleSystem;
    [SerializeField] private Transform orientationTransform;
    [SerializeField] private float moveForce;
    [SerializeField] private Transform Graphic;
    [SerializeField] private float GraphicRotationTimer;
    [SerializeField] private float MaxVelocity;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private GameObject stepUpLower;
    [SerializeField] private GameObject stepUpUpper;
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepSmooth = 0.1f;
    public Vector2 InputDirection;
    Rigidbody rb;
    PlayerInput PlayerInput;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerInput = GetComponent<PlayerInput>();
        stepUpUpper.transform.position = new Vector3(stepUpUpper.transform.position.x, stepHeight, stepUpUpper.transform.position.z);
        PlayerInput.OnRun += RunCheck;
    }

 
    private void FixedUpdate()
    {
        MovePlayer();

    }


    private void LateUpdate(){
        if(InputDirection !=Vector2.zero)
            GraphicForwardSet();
    }

    private Vector3 MoveDirection(){
        Vector3 moveDirection = (orientationTransform.forward * InputDirection.y 
            + orientationTransform.right * InputDirection.x).normalized;
        return moveDirection;
    }

    private void GraphicForwardSet()
    {
        Graphic.transform.rotation = Quaternion.Slerp(Graphic.transform.rotation,Quaternion.LookRotation(orientationTransform.forward), GraphicRotationTimer*Time.deltaTime);

    }

    private void MovePlayer()
    {
        if (InputDirection != Vector2.zero) {
            rb.AddForce(MoveDirection() * moveForce, ForceMode.Acceleration);
            movementParticleSystem.SetActive(true);
            StepClimb();
            SetMaxSpeed();



        }
        else {

            rb.velocity = Vector3.Slerp(rb.velocity, Vector3.zero, 0.05f);
            movementParticleSystem.SetActive(false);
        }
    }

    private void SetMaxSpeed()
    {
        if (rb.velocity.magnitude > MaxVelocity) 
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity);
    }
    
    private void RunCheck(bool canRun)
    {
        if (canRun) {
            MaxVelocity = RunSpeed;

        }
        else
            MaxVelocity = WalkSpeed;
    }

    void StepClimb()
    {

        RaycastHit hitLower;
        if(Physics.Raycast(stepUpLower.transform.position,transform.TransformDirection(stepUpLower.transform.forward),out hitLower, 0.1f)){

            RaycastHit hitUpper;

            if(!Physics.Raycast(stepUpUpper.transform.position,transform.TransformDirection(stepUpUpper.transform.forward), out hitUpper, 0.2f)){
                rb.position -= new Vector3(0,-stepSmooth, 0);
                Debug.Log("CanTransform");
                rb.velocity =new Vector3(rb.velocity.x*1.1f,rb.velocity.y,rb.velocity.z*1.1f);
            }

        }

    }



    

}
