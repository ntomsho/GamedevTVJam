using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPylon : BaseInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    public event EventHandler OnPylonBuilt;
    public event EventHandler OnPylonDestroyed;
    WorldType worldType = WorldType.Technology;

    public override void Interact(CharacterInteraction character)
    {
        return; // Has no interaction
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
