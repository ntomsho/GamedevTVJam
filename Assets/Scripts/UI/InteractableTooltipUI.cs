using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableTooltipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactableNameText;
    [SerializeField] private TextMeshProUGUI interactableMainActionText;
    [SerializeField] private TextMeshProUGUI interactableSecondaryActionText;

    public void SetInteractable(IInteractable interactable)
    {
        interactableNameText.text = interactable.GetInteractableSO().Name;
        interactableMainActionText.text = interactable.GetInteractableSO().MainActionName;
        interactableSecondaryActionText.text = interactable.GetInteractableSO().SecondaryActionName;
    }
}
