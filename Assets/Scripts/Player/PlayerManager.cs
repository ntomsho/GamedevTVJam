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

    public static PlayerManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one PlayerManager - " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Pause()
    {
        characterInteraction.enabled= false;
        characterController.enabled = false;
        thirdPersonController.enabled = false;
    }

    public void Resume()
    {
        characterInteraction.enabled = true;
        characterController.enabled = true;
        thirdPersonController.enabled = true;

    }
}

