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
    }



    private void CharacterInteraction_OnInteractionStarted(object sender, CharacterInteraction.OnInteractionStartedEventArgs e)
    {
        animator.SetInteger("InteractAction", e.interactionType);
        animator.SetTrigger("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
