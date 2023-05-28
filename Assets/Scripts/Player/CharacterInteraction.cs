using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] ThirdPersonController playerController;
    [SerializeField] LayerMask interactableLayer;

    [SerializeField] GameObject interactableTooltipPrefab;
    
    private IInteractable currentInteractable;
    private GameObject interactableTooltip;

    bool isInteracting = false;
    float interactionTimer = 0f;
    float timerDrainSpeed = 0.2f;

    private void Awake()
    {
        interactableTooltip = Instantiate(interactableTooltipPrefab);
        interactableTooltip.SetActive(false);
    }

    public bool GetIsInteracting()
    {
        return isInteracting;
    }

    public void SetIsInteracting(bool value)
    {
        isInteracting = value;
    }

    public float GetInteractionTimer()
    {
        return interactionTimer;
    }

    public IInteractable GetCurrentInteractable()
    {
        return currentInteractable;
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            isInteracting = true;
            if (currentInteractable.GetTimeToInteract() == 0f) // Interact immediately
            {
                currentInteractable.Interact(this); //validation?
                isInteracting = false;
            } else // Start the timer
            {
                isInteracting = true;
            }
        }

        if (isInteracting && Input.GetMouseButton(0))
        {
            interactionTimer += Time.deltaTime;
            if (interactionTimer > currentInteractable.GetTimeToInteract())
            {
                currentInteractable.Interact(this); //validation?
                isInteracting = false;
                interactionTimer = 0f;
            }
        } else
        {
            isInteracting = false;
        }
    }

    void Update()
    {

        HandleInteraction();
        HandleInput();
    }

    private void HandleInteraction()
    {
        Vector3 cameraDirection = Camera.main.transform.forward;

        Debug.DrawRay(playerTransform.position, cameraDirection * 3f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, cameraDirection, out hit, 3f, interactableLayer) && !isInteracting)
        {
            currentInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            currentInteractable.SetHighlight(true);

            interactableTooltip.transform.position = hit.point;
            interactableTooltip.SetActive(true);
            interactableTooltip.GetComponent<InteractableTooltipUI>().SetInteractable(currentInteractable);
        }
        else if (!isInteracting)
        {
            if (currentInteractable != null) currentInteractable.SetHighlight(false);
            currentInteractable = null;
            interactableTooltip.SetActive(false);
        }
        if (!isInteracting)
        {
            interactionTimer = 0f;
            // if (interactionTimer > 0f) interactionTimer -= Time.deltaTime * timerDrainSpeed;
            // else interactionTimer = 0f;
        }
    }
}
