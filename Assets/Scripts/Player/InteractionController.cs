using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private CharacterInteraction characterInteraction;

    // Start is called before the first frame update
    void Start()
    {
        characterInteraction= GetComponent<CharacterInteraction>();

        characterInteraction.OnInteractionStarted += CharacterInteraction_OnInteractionStarted;
        characterInteraction.OnInteractionCompleted += CharacterInteraction_OnInteractionCompleted;
    }


    private void CharacterInteraction_OnInteractionStarted(object sender, CharacterInteraction.OnInteractionStartedEventArgs e)
    {
        if (e.interactionType == 1)
        {
            animator.SetBool("Interact", true);
        } else
        {
            animator.SetBool("FinishQuest", true);
        }
    }

    private void CharacterInteraction_OnInteractionCompleted(object sender, EventArgs e)
    {
        animator.SetBool("Interact", false);
        animator.SetBool("FinishQuest", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
