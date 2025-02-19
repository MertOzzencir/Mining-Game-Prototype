
using UnityEngine;

public enum BreakableType {
    Stone,
    Tree
}
public interface IBreakable 
{

    public BreakableType Type { get; set; }
    void HitAnimation();

    void DestroyCondition(float Damage,Vector3 forceDirection);

    void ParticleAnimationHandler(Vector3 positionToInstantiate);
}
