using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // Interact with the object;
    public void Interact(CharacterInteraction character);

    // Highlight object and generate info popup when moused over;
    public void SetHighlight(bool value);

    // Return the object's renderer for the dual object controller
    public Renderer GetRenderer();
}
