using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public static AnimationEventScript instance;
    public PlayerMovement playerMovement;
    private static IBreakable BreakObject;
    private static Vector3 Position;
    private static float Damage;

    private void Awake()
    {
        instance = this;
        playerMovement= GetComponentInParent<PlayerMovement>();
    }
    public void SetValues(IBreakable BreakableObject,Vector3 ParticlePosition,float damage)
    {
        BreakObject = BreakableObject;
        Position = ParticlePosition;
        Damage = damage;
    }

    public void DigAnimation()
    {
        BreakObject.DestroyCondition(Damage, Position);

    }

    public void InputEnable()
    {
        playerMovement.enabled  = true;
    }

    public void InputDisabled()
    {
        playerMovement.enabled = false;
    }
}
