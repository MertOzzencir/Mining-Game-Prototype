using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    public static bool hasTraveled;
    public int FloorYPosition;
    public Transform Elevator;
    public Transform Player;
    [SerializeField] private Transform[] objectsToAnimate;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" ) {
            ObjectAnimatiorManager.Instance.RotateObject(objectsToAnimate[0], 0, -90, 0, 0.5f);
            ObjectAnimatiorManager.Instance.RotateObject(objectsToAnimate[1], 0, 90, 0, 0.5f);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasTraveled) {
            ObjectAnimatiorManager.Instance.RotateObject(objectsToAnimate[0], 0, 180, 0, 0.5f);
            ObjectAnimatiorManager.Instance.RotateObject(objectsToAnimate[1], 0, 180, 0, 0.5f);
            if(Vector3.Distance(Elevator.position,Player.position) < 10f) {
                StartCoroutine(ElevatorMove.instance.MoveElevator(FloorYPosition));
                hasTraveled = true;
                StartCoroutine(ElevatorTimer());

            }



        }
    }

    IEnumerator ElevatorTimer()
    {
        yield return new WaitForSeconds(15f);
        hasTraveled = false;
    }
}
