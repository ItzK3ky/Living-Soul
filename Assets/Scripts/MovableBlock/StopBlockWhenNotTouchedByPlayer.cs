using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBlockWhenNotTouchedByPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isTouchingPlayerLeft() && !isTouchingPlayerRight())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    #region Surroundings checks

    private bool isTouchingPlayerLeft()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.01f, playerLayer);
    }
    private bool isTouchingPlayerRight()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, 0.01f, playerLayer);
    }

	#endregion
}
