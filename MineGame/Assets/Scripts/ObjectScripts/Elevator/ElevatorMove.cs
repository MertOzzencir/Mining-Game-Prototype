using System.Collections;
using UnityEngine;

public class ElevatorMove : MonoBehaviour {

    public static ElevatorMove instance;
    public Transform elevator; 
    public float moveSpeed = 2f; 
    public float targetHeight = -10f; 
    public bool isActivated = false;

    private void Start()
    {
        instance = this;
    }
   
    public IEnumerator MoveElevator(float floorPosition)
    {
        yield return new WaitForSeconds(1.5f);
        do {
            Vector3 newPosition = elevator.position;
            newPosition.y = Mathf.MoveTowards(elevator.position.y, floorPosition, moveSpeed * Time.deltaTime);
            elevator.position = newPosition;
            yield return null;
        } while (elevator.position.y != floorPosition);
    }
    public void ActivateElevator()
    {
        isActivated = true;
    }
}
