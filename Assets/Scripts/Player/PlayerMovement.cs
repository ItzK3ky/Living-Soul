using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Attributes")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpPower;

    [Header("Layers")]
    [SerializeField] private LayerMask mapLayer;
    [SerializeField] private LayerMask jumpableLayers;

    //Components
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
		#region Movement + related input
		if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            if(Physics2D.gravity.y < 0)
                rb.velocity += new Vector2(0, 1 * jumpPower);
            else
                rb.velocity += new Vector2(0, -1 * jumpPower);
        }
		#endregion
	}

	void FixedUpdate()
    {
		    #region Rigidbody-based Movement + related input
		    if (Input.GetKey(KeyCode.A) && !isTouchingWallLeft())
        {
            rb.velocity = new Vector2(-10 * playerSpeed * Time.fixedDeltaTime, rb.velocity.y);

            FlipPlayer(true);
        }
        if (Input.GetKey(KeyCode.D) && !isTouchingWallRight())
        {
            rb.velocity = new Vector2(10 * playerSpeed * Time.fixedDeltaTime, rb.velocity.y);

            FlipPlayer(false);
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))   //No dashing while not moving
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        #endregion
    }

    private void FlipPlayer(bool flipLeft)
    {
        if(Physics2D.gravity.y < 0)
            if (flipLeft)
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            else
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        else
            if (flipLeft)
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            else
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
    }

    #region Player Checks
    private bool isTouchingWallLeft()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, 0.01f, mapLayer);
    }
    private bool isTouchingWallRight()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, 0.01f, mapLayer);
    }

	private bool IsGrounded()
    {
        if(Physics2D.gravity.y < 0)
            if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.05f, jumpableLayers))
                return true;
            else
                return false;
        else
            if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, 0.05f, jumpableLayers))
                return true;
            else
                return false;
    }
	#endregion
}


