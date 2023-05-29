using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public class CharacterInteraction : MonoBehaviour
{
    public event EventHandler<OnInteractionStartedEventArgs> OnInteractionStarted;
    public event EventHandler OnInteractionCompleted;

    public class OnInteractionStartedEventArgs : EventArgs
    {
        public int interactionType;
    }

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform interactTransform;
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
        IGrowOverTime growOverTime = currentInteractable as IGrowOverTime;
        bool canInteract = (growOverTime == null || growOverTime.GetIsGrown());

        if (Input.GetMouseButtonDown(0) && currentInteractable != null && canInteract)
        {
            SetIsInteracting(true);

            bool isQuestInteraction = currentInteractable.GetIsQuestBuilding();

            OnInteractionStarted?.Invoke(this, new OnInteractionStartedEventArgs() { interactionType = isQuestInteraction ? 2 : 1 });

            if (currentInteractable.GetTimeToInteract() == 0f) // Interact immediately
            {
                currentInteractable.Interact(this); //validation?
                SetIsInteracting(false);

                OnInteractionCompleted?.Invoke(this, EventArgs.Empty);
            } else // Start the timer
            {
                SetIsInteracting(true);
            }
        }

        if (isInteracting && Input.GetMouseButton(0))
        {
            interactionTimer += Time.deltaTime;
            if (interactionTimer > currentInteractable.GetTimeToInteract())
            {
                currentInteractable.Interact(this); //validation?
                SetIsInteracting(false);
                interactionTimer = 0f;

                OnInteractionCompleted?.Invoke(this, EventArgs.Empty);
            }
        } else
        {
            SetIsInteracting(false);
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

        Debug.DrawRay(interactTransform.position, cameraDirection * 3f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(interactTransform.position, cameraDirection, out hit, 3f, interactableLayer) && !isInteracting)
        {
            currentInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (currentInteractable != null)
            {
                currentInteractable.SetHighlight(true);
                interactableTooltip.transform.position = hit.point;
                interactableTooltip.SetActive(true);
                interactableTooltip.GetComponent<InteractableTooltipUI>().SetInteractable(currentInteractable);
            }
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
