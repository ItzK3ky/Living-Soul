using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    [Header("Button Attributes")]
    [SerializeField] private float buttonPressedDurationInSec;
    [Space]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private SpriteRenderer spriteRenderer;

    void Start()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Only called by Event
    public override void Interact(Component sender, object data)
    {
        //Check if this object is meant to be interacted with
        if ((GameObject) data == gameObject)
        {
            //Only be pressable, if not already activated
            if (!currentlyActivated)        
            {
                onInteract.Raise(this, 1);
                ChangeButtonColor(activeColor);

                currentlyActivated = true;

                StartCoroutine(UnpressButton());
            }
        }
    }

    private void ChangeButtonColor(Color color)
    {
        spriteRenderer.color = color;
    }

    private IEnumerator UnpressButton()
    {
        //Wait for Duration
        yield return new WaitForSeconds(buttonPressedDurationInSec);

        //Call Event -> Button unpressed
        onUninteract.Raise(this, 0);
        ChangeButtonColor(inactiveColor);

        currentlyActivated = false;

    }
}
