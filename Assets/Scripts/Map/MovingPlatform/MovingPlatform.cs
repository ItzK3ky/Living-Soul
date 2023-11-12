using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Platform Attributes")]
    [SerializeField] private float platformSpeed;
    [SerializeField] private float platformWaitTimeInSec;
    [SerializeField] private int startingDirection;

    private int horizontalMovingDirection;

    [Header("Start and end position")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    //Components
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (startingDirection == 0)
            Debug.LogError("This platform has no set starting direction!");
        else
            horizontalMovingDirection = startingDirection;
    }

    void Update()
    {
        //Check if the start/end position is reached
        if(transform.position.x <= startPosition.position.x)
        {
            horizontalMovingDirection = 0;
            StartCoroutine(ChangeMovingDirection(1));
        } else if (transform.position.x >= endPosition.position.x)
        {
            horizontalMovingDirection = 0;
            StartCoroutine(ChangeMovingDirection(-1));
        }
    }

    void FixedUpdate()
    {
        float verticalMovement = 0;
        if (horizontalMovingDirection == -1)
            verticalMovement = (startPosition.position.y - transform.position.y) * platformSpeed * Time.fixedDeltaTime;
        else if (horizontalMovingDirection == 1)
            verticalMovement = (endPosition.position.y - transform.position.y) * platformSpeed * Time.fixedDeltaTime;

        rb.velocity = new Vector2(horizontalMovingDirection * platformSpeed * Time.fixedDeltaTime, verticalMovement);
    }

    private IEnumerator ChangeMovingDirection(int newMovingDirection)
    {
        yield return new WaitForSeconds(platformWaitTimeInSec);

        horizontalMovingDirection = newMovingDirection;
    }
}
