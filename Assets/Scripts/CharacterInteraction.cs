using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] ThirdPersonController playerController;
    [SerializeField] LayerMask interactableLayer;
    IInteractable currentInteractable;

    bool canInteract = true;

    public bool GetCanInteract()
    {
        return canInteract;
    }

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }

    void Update()
    {
        Vector3 cameraDirection = Camera.main.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, cameraDirection, out hit, 5f, interactableLayer) && canInteract)
        {
            currentInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            currentInteractable.SetHighlight(true);

            if (Input.GetMouseButtonDown(0) && currentInteractable != null)
            {
                // TODO: resource and player state validation
                currentInteractable.Interact(this);
            }
        } else
        {
            if (currentInteractable != null) currentInteractable.SetHighlight(false);
            currentInteractable = null;
        }
    }
}
