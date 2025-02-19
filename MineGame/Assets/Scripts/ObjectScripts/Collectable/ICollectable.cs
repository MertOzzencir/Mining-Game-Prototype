
using UnityEngine;


public enum MaterialType {
    Silver,
    Wood,
    Coal
}
public interface ICollectable 
{
    public MaterialType Type { get; set; }
    void Collect(Vector3 positionToCollect);
    void HandleAnimation();
    
}
