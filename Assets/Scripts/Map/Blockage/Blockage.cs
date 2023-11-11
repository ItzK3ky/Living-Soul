using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockage : MonoBehaviour
{
    [Header("Blockage Attributes")]
    [SerializeField] private GameObject linkedInteractable;
    [SerializeField] private float raisingSpeed;

    [Header("Start and end position")]
    [SerializeField] private Transform startPosition;   //Should be the lower position
    [SerializeField] private Transform endPosition;     //Should be upper position

    //Components
    private new Transform transform;

    private bool currentlyRaised = false;
    private bool currentlyRaising = false;
    private bool currentlyLowering = false;

    void Start()
    {
        if (linkedInteractable == null)
            Debug.LogError("No linked interactable set for " + gameObject);

        transform = GetComponent<Transform>();
    }

    void Update()
    {
		#region Check if start/end position is reached
		if (transform.position.y <= startPosition.position.y)
        {
            currentlyLowering = false;
            currentlyRaised = false;
        } else if (transform.position.y >= endPosition.position.y)
        {
            currentlyRaising = false;
            currentlyRaised = true;
        }
		#endregion

		#region Raising and lowering movement
		if (currentlyRaising)
            transform.position = new Vector3(transform.position.x, transform.position.y + raisingSpeed * Time.deltaTime, transform.position.z);
    
        if (currentlyLowering)
            transform.position = new Vector3(transform.position.x, transform.position.y - raisingSpeed * Time.deltaTime, transform.position.z);
		#endregion
	}

    //Only called by event
	public void ChangeState(Component sender, object data)
    {
        if (sender.gameObject == linkedInteractable)
        {
            if (currentlyLowering || !currentlyRaised)
            {
                currentlyLowering = false;
                currentlyRaising = true;
            } else if (currentlyRaising || currentlyRaised)
            {
                currentlyRaising = false;
                currentlyLowering = true;
            }
        }
    }
}
