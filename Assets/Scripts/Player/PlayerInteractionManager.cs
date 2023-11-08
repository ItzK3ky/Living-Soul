using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] private GameEvent onPlayerInteractWithInteractable;

    private Collider2D interactableCollider;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
            if (interactableCollider != null)
                onPlayerInteractWithInteractable.Raise(this, interactableCollider.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Detecting interactables
        //Detect interactable gameobjects, when *entering* their (trigger) collider
        if (collision.gameObject.layer == 9)
            interactableCollider = collision;
        #endregion
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        #region (Un-)detecting Interactables
        //Detect interactable gameobjects, when *leaving* their (trigger) collider
        if (collision == interactableCollider)
            interactableCollider = null;
        #endregion
    }
}
