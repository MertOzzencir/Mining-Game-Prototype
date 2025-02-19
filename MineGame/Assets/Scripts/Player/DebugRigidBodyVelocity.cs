using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugRigidBodyVelocity : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SpeedText;
    [SerializeField] private Rigidbody Speed;


    void Update()
    {
        
        SpeedText.text = Speed.velocity.magnitude.ToString();
    }
}
