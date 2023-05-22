using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDualObjectChild
{
    // Return the object's renderer for the dual object controller
    public Renderer GetRenderer();

    // Destroy the dual object parent
    public void DestroyDualObject();
}
