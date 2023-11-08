using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float knockback;

    protected int walkingDirection;

    [Header("Start and End Position")]
    [SerializeField] protected Transform startPosition;
    [SerializeField] protected Transform endPosition;

    [Header("Game Events")]
    [SerializeField] protected GameEvent killPlayerEvent;
    [SerializeField] protected GameEvent playerGetKnockbackEvent;

    [Space]

    [SerializeField] protected Rigidbody2D rb;

    void Start()
    {
		#region Random starting walking direction
		if (Random.Range(-1, 1) < 0)
            walkingDirection = -1;
        else
            walkingDirection = 1;
		#endregion
	}
    void Update()
    {
        #region Check if start/end position is reached

        if (transform.position.x <= startPosition.position.x)
            walkingDirection = 1;
        else if (transform.position.x >= endPosition.position.x)
            walkingDirection = -1;

		#endregion
	}

	void FixedUpdate()
    {
        //Enemy movement
        rb.velocity = new Vector2(walkingDirection * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //Kill Player, if collided with player (player only dies if not currently dashing)
        if (collision.gameObject.layer == 8)
            killPlayerEvent.Raise(this, null);
    }

    //Called by PlayerManager (KillPlayer() func)
    public void KillEnemy(Component sender, object data)
    {
        //Get direction of player for knockback
        int direction;
        if (sender.transform.position.x - transform.position.x > 0)
            direction = 1;
        else
            direction = -1;

        playerGetKnockbackEvent.Raise(this, knockback * direction);

        GameObject.Destroy(gameObject);
    }
}
