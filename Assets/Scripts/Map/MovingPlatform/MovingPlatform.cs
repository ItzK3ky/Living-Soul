using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Platform Attributes")]
    [SerializeField] private float platformSpeed;
    [SerializeField] private float platformWaitTimeInSec;
    [SerializeField] private int startingDirection;

    [Header("Start and end position")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    private Vector3 moveTowards;

    void Start()
    {

        if (startingDirection == 0)
            Debug.LogError("This platform has no set starting direction!");
        else if (startingDirection == -1)
            moveTowards = startPosition.position;
        else if (startingDirection == 1)
            moveTowards = endPosition.position;
        else
            Debug.Log("This starting direction is invalid. Starting direction can only be 1 (right) or -1 (left)");
    }

    void Update()
    {
        //Check if the start/end position is reached
        if(transform.position == startPosition.position)
        {
            StartCoroutine(ChangeMovingDirection(endPosition.position));
        } else if (transform.position == endPosition.position)
        {
            StartCoroutine(ChangeMovingDirection(startPosition.position));
        }

        transform.position = Vector3.MoveTowards(transform.position, moveTowards, platformSpeed * Time.deltaTime);
    }

    private IEnumerator ChangeMovingDirection(Vector3 newMoveTowards)
    {
        yield return new WaitForSeconds(platformWaitTimeInSec);

        moveTowards = newMoveTowards;
    }
}
