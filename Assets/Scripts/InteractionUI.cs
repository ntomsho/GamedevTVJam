using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] CharacterInteraction characterInteractionController;
    [SerializeField] Image uiImage;
    [SerializeField] float timeToFade = 0.5f;
    [SerializeField] float imageExpansion = 0.2f;
    bool uiIsAnimating;

    public void UpdateInteractionUIFill(float value, float max)
    {
        uiImage.fillAmount = value / max;
    }

    void CompleteFill()
    {
        uiIsAnimating = true;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime * timeToFade;
            uiImage.rectTransform.localScale = Vector3.one * (Time.deltaTime * imageExpansion);
            yield return null;
        }
        uiImage.enabled = false;
        uiIsAnimating = false;
        canvasGroup.alpha = 1;
    }

    void Update()
    {
        // TODO: Make this not suck
        float interactionTimer = characterInteractionController.GetInteractionTimer();
        float interactionDuration = characterInteractionController.GetCurrentInteractable() != null ? characterInteractionController.GetCurrentInteractable().GetTimeToInteract() : 0f;
        if (interactionTimer > 0f)
        {
            uiImage.enabled = true;
            UpdateInteractionUIFill(interactionTimer, interactionDuration);
            if (interactionTimer >= interactionDuration)
            {
                CompleteFill();
            }
        } else
        {
            uiImage.enabled = false;
        }
    }
}
