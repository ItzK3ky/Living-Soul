using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("General Player Attributes")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float knockedBackTimeInSec;

    [Header("Layers")]
    [SerializeField] private LayerMask mapLayer;
    [SerializeField] private LayerMask movableBlockLayer;

    [Header("Game Events")]
    [SerializeField] private GameEvent onPlayerInteractWithInteractable;

    //Components
    private Rigidbody2D rb;
    private BoxCollider2D coll; 
    
    //Other variables
    private Collider2D interactableCollider;
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

		#region Other Input
		if (Input.GetButtonDown("Interact"))
        {
            if (interactableCollider != null)
            {
                //Interact
                onPlayerInteractWithInteractable.Raise(this, interactableCollider.gameObject);
            }
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

	private void OnTriggerEnter2D(Collider2D collision)
    {
		#region Detecting interactable related
		//Detect interactable gameobjects, when *entering* their (trigger) collider
		if (collision.gameObject.layer == 9)
            interactableCollider = collision;
		#endregion
	}
	private void OnTriggerExit2D(Collider2D collision)
    {
        #region (Un-)detecting Interactable related
        //Detect interactable gameobjects, when *leaving* their (trigger) collider
        if (collision == interactableCollider)
            interactableCollider = null;
		#endregion
	}

    //Only called by event
    public void KillPlayer(Component sender, object data)
    {
        GameObject.Destroy(gameObject);
    }

    //Only called by event
    public void GetKnockback(Component sender, object data)
    {
        isKnockedBack = true;
        rb.velocity = new Vector2((float)data, Mathf.Abs((float)data));
        StartCoroutine(StopKnockback());     //Needed, to stop update methods from overwriting the knockback velocity instantly
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
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.05f, mapLayer)
            || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.05f, movableBlockLayer))
            return true;
        else return false;
    }
	#endregion

	#region Knockback (time) related
    //Needed, to stop update methods from overwriting the knockback velocity instantly
    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(knockedBackTimeInSec);
        isKnockedBack = false;
    }
	#endregion
}


