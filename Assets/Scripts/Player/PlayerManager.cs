using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private CharacterInteraction characterInteraction;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private ThirdPersonController thirdPersonController;

    private InteractionController interactionController;

    public static PlayerManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one PlayerManager - " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        interactionController = GetComponent<InteractionController>();

        Instance = this;
    }

    public InteractionController GetInteractionController() { return interactionController; }
    public CharacterInteraction GetCharacterInteraction() { return characterInteraction; }

    public void Pause()
    {
        characterInteraction.enabled= false;
        characterController.enabled = false;
        thirdPersonController.enabled = false;
        interactionController.enabled = false;
    }

    public void Resume()
    {
        characterInteraction.enabled = true;
        characterController.enabled = true;
        thirdPersonController.enabled = true;
        interactionController.enabled= true;
    }
}

