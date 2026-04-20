using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int currentFloor;
    public int destinationFloor;
    public int currentDirection;
    public int nextDirection;

    public float moveSpeed = 5;
    public Transform[] floorPoints;
    
    private CharacterController characterController;
    
    public List<int> floorRequest = new List<int>();
    
    public List<int> upFloorRequests = new List<int>();
    public List<int> downFloorRequests = new List<int>();

    public bool isElevatorMoving;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentFloor = 0;

    }

    private void Update()
    {
        if (floorRequest.Count > 0)
        {
            UpdateDestination();
            isElevatorMoving = true;
            if (isElevatorMoving)
            {
                if (Vector3.Distance(transform.position, floorPoints[destinationFloor].transform.position) > 0.01f)
                {
                    isElevatorMoving = true;
                    Debug.Log("Elevator moving");
                    transform.position = Vector3.MoveTowards(transform.position,
                        floorPoints[destinationFloor].transform.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    isElevatorMoving = false;
                    OnDestinationReach();
                }
            }
        }
    }

    private void OnDestinationReach()
    {
        if(floorRequest.Contains(destinationFloor))
        {
            floorRequest.Remove(destinationFloor);
            Debug.Log("destination updated");
        }
    }
    private void UpdateDestination()
    {
        destinationFloor = floorRequest[0];
    }
    public void OnClickElevatorButton(int destination)
    {
        if (!floorRequest.Contains(destination))
        {
            floorRequest.Add(destination);
        }
    }

    public void OnClickDirectionButton(int direction)
    {
        nextDirection = direction;
    }
}
