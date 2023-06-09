using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    // Interact with the object;
    public void Interact(CharacterInteraction character);

    // Get time needed to complete interaction
    public float GetTimeToInteract();

    public InteractableSO GetInteractableSO();

    // Highlight object and generate info popup when moused over;
    public void SetHighlight(bool value);

    public bool GetIsQuestBuilding();
}
