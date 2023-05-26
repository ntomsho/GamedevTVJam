using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] Outline highlightOutline;
    [SerializeField] InteractableSO interactableSO;

    public abstract void Interact(CharacterInteraction character);

    public float GetTimeToInteract()
    {
        return interactableSO.TimeToInteract;
    }

    public void SetHighlight(bool value)
    {
        highlightOutline.enabled = value;
    }

    public InteractableSO GetInteractableSO()
    {
        return interactableSO;
    }
}
