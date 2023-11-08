using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeGravityAbility : MonoBehaviour
{
    [Header("Ability Attributes")]
    [SerializeField] private float cooldownInSec;

    [Header("Game Events")]
    [SerializeField] private GameEvent gravityChangedEvent;

    private SpriteRenderer spriteRenderer;

    private Vector2 downwardGravity = new Vector2(0, -9.81f);
    private Vector2 upwardGravity = new Vector2(0, 9.81f);

    private bool canChangeGravity = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        #region Related Input
        if (Input.GetButtonDown("Ability"))
        {
            SwitchGravityUpsideDown();
        }
		#endregion
	}

    private void SwitchGravityUpsideDown()
    {
        if (!canChangeGravity)
            return;

        if (Physics2D.gravity == downwardGravity)
        {
            Physics2D.gravity = upwardGravity;
            gravityChangedEvent.Raise(this, 1);
        }
        else
        {
            Physics2D.gravity = downwardGravity;
            gravityChangedEvent.Raise(this, -1);
        }

        FlipPlayer();

        canChangeGravity = false;
        StartCoroutine(AbilityCooldown());
    }

    private void FlipPlayer()
    {
        transform.Rotate(180, 0, 0);
    }

    private IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(cooldownInSec);
        canChangeGravity = true;
    }
}
