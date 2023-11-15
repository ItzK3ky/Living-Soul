using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] private bool movableByPlayer;
    [SerializeField] private bool movableByNPC;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask npcLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!IsTouchingPlayer() && !IsTouchingNpc())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        Debug.Log("is Touching npc: " + IsTouchingNpc());
        Debug.Log("movabbababab: " + movableByNPC);
        if (IsTouchingNotMover())
        {
            if (!IsTouchingMover())
            {
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            } else
            {
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            }
        } else
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
    }

    #region Surroundings checks

    private bool IsTouchingMover()
    {
        if((IsTouchingPlayer() && movableByPlayer) || (IsTouchingNpc() & movableByNPC))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool IsTouchingNotMover()
    {
        if ((IsTouchingPlayer() && !movableByPlayer) || (IsTouchingNpc() & !movableByNPC))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsTouchingPlayer()
    {
        if(Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, 0.02f, playerLayer) || Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.02f, playerLayer))
        {
            return true;
        } else
        {
            return false;
        }
    }
    private bool IsTouchingNpc()
    {
        if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, 0.02f, npcLayer) || Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.02f, npcLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}
