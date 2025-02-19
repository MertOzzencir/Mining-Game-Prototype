using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class ObjectAnimatiorManager : MonoBehaviour
{
    public static ObjectAnimatiorManager Instance;
    private void Start()
    {
        Instance = this;
    }
    public void RotateObject(Transform rotatedObject,float x,float y,float z,float rotateTime)
    {
        rotatedObject.DORotate(new Vector3(x, y, z), rotateTime);
    }
    public void ScaleObject(Transform scaledObject,Vector3 scaleValue, float timer)
    {
        scaledObject.DOScale(scaleValue, timer);
    }

    public void LocalMoveObject(Transform movedObject,Vector3 positionToMove,float TimeToApply)
    {
        movedObject.DOMove(positionToMove, TimeToApply);

    }
}
