using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    [Header("NPC Attributes")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float waitTimeBeforeDieInSec;
    [Space]
    [SerializeField] private Transform endPosition;

    private float movementDirection;

    //Components
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _ = (transform.position.x - endPosition.position.x) < 0 ? movementDirection = 1 : movementDirection = -1;
    }

    void Update()
    {
        if(Mathf.RoundToInt(Mathf.Abs(transform.position.x - endPosition.position.x)) <= 0)
        {
            movementDirection = 0;
            StartCoroutine(KillNpc());
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movementDirection * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private IEnumerator KillNpc()
    {
        yield return new WaitForSeconds(waitTimeBeforeDieInSec);

        Destroy(gameObject);
    }
}
