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
    WorldType worldType = WorldType.Technology;

    public void Interact(CharacterInteraction character)
    {
        return; // Has no interaction
    }

    public float GetTimeToInteract()
    {
        return 0;
    }

    public void SetHighlight(bool value)
    {
        //TODO: Get an object highlight shader
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
