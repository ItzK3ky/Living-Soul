using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [Header("Lever Attributes")]
    [SerializeField] private Color activatedColor;
    [SerializeField] private Color unactivatedColor;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Only called by event
    public override void Interact(Component sender, object data)
    {
        //Check if this object is meant to be interacted with
        if ((GameObject)data == gameObject)
        {
            if (!currentlyActivated)
            {
                onInteract.Raise(this, 1);
                ChangeLeverColor(activatedColor);

                currentlyActivated = true;
            }
            else
            {
                onUninteract.Raise(this, 0);
                ChangeLeverColor(unactivatedColor);

                currentlyActivated = false;
            }
        }
    }

    private void ChangeLeverColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
