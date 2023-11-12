using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPlatform : MonoBehaviour
{
    [Header("Gravity Platform Attributes")]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [Space]
    [SerializeField] private bool activeWhenGravityIsFlipped;

    //Components
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        bool gravityIsFlipped;
        _ = Physics2D.gravity.y > 0 ? gravityIsFlipped = true : gravityIsFlipped = false;

        if(gravityIsFlipped != activeWhenGravityIsFlipped)
        {
            coll.enabled = false;
            ChangeColor(inactiveColor);
        } else
        {
            coll.enabled = true;
            ChangeColor(activeColor);
        }
    }

    private void ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
