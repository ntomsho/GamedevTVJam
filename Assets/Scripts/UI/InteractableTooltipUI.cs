using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableTooltipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactableNameText;
    [SerializeField] private TextMeshProUGUI interactableDescriptionText;
    [SerializeField] private TextMeshProUGUI interactableMainActionText;
    [SerializeField] private TextMeshProUGUI interactableSecondaryActionText;
    bool interactableIsReady;

    public void SetInteractable(IInteractable interactable)
    {
        IGrowOverTime growOverTime = interactable as IGrowOverTime;
        interactableIsReady = (growOverTime == null || growOverTime.GetIsGrown());

        interactableNameText.text = interactable.GetInteractableSO().Name;
        SetInteractableDescription(interactable);
        Debug.Log(interactable.GetInteractableSO().MainActionName);
        SetMainActionText(interactable.GetInteractableSO().MainActionName);
        interactableSecondaryActionText.text = interactable.GetInteractableSO().SecondaryActionName;
    }

    void SetInteractableDescription(IInteractable interactable)
    {
        if (interactableIsReady)
        {
            interactableDescriptionText.color = Color.white;
            interactableDescriptionText.text = interactable.GetInteractableSO().IsReadyDescription;
        } else
        {
            interactableDescriptionText.color = Color.red;
            interactableDescriptionText.text = interactable.GetInteractableSO().IsNotReadyDescription;
        }
    }

    void SetMainActionText(string actionText)
    {
        if (interactableIsReady)
        {
            interactableMainActionText.color = Color.white;
        } else
        {
            interactableMainActionText.color = Color.red;
        }
        Debug.Log(actionText);
        interactableMainActionText.text = actionText;
        Debug.Log(interactableMainActionText.text);
    }
}
