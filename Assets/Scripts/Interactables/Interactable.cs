using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] protected GameEvent onInteract;
    [SerializeField] protected GameEvent onUninteract;

    protected bool currentlyActivated = false;

    //Only called by Event
    public virtual void Interact(Component sender, object data)
    {
    }
}
