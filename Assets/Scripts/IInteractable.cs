using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact(CharacterInteraction character);

    // Highlight object and generate info popup when moused over;
    public void SetHighlight(bool value);
}
