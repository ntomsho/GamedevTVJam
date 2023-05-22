using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPylon : IInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    public event EventHandler OnPylonBuilt;
    public event EventHandler OnPylonDestroyed;
    public void Interact(CharacterInteraction character)
    {
        
    }

    public void SetHighlight(bool value)
    {
        
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }
}
