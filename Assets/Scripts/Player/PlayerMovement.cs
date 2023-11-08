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
    
    //Other variables
    private bool isKnockedBack;    //Needed, to stop update methods from overwriting the knockback velocity instantly

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
		#region Movement + related input
		if (Input.GetButtonDown("Jump") && IsGrounded() && !isKnockedBack)
        {
            rb.velocity += new Vector2(0, 1 * jumpPower);
        }
		#endregion
	}

	void FixedUpdate()
    {
		#region Rigidbody-based Movement + related input
		if (Input.GetKey(KeyCode.A) && !isTouchingWallLeft() && !isKnockedBack)
        {
            rb.velocity = new Vector2(-10 * playerSpeed * Time.fixedDeltaTime, rb.velocity.y);

            FlipPlayer(true);
        }
        if (Input.GetKey(KeyCode.D) && !isTouchingWallRight() && !isKnockedBack)
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
        if (flipLeft)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.rotation = new Quaternion(0, 0, 0, 0);
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
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.05f, jumpableLayers))
            return true;
        else
            return false;
    }
	#endregion
}


