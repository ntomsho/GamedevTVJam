using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] Outline highlightOutline;
    [SerializeField] InteractableSO interactableSO;

    public abstract void Interact(CharacterInteraction character);

    public virtual float GetTimeToInteract()
    {
        return interactableSO.TimeToInteract;
    }

    public virtual void SetHighlight(bool value)
    {
        highlightOutline.enabled = value;
    }

    public InteractableSO GetInteractableSO()
    {
        return interactableSO;
    }

    public virtual bool GetIsQuestBuilding()
    {
        return false;
    }
}
