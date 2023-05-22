using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IInteractable
{
    public void Interact(CharacterInteraction character)
    {
        // Set player to start chopping wood, on complete, destroy this and add wood to inventory or spawn wood pickups
    }

    public void SetHighlight(bool value)
    {
        
    }
}
