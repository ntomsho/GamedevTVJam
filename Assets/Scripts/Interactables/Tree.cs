using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject woodPickupPrefab;
    [SerializeField] float timeToChop = 2f;
    int numWoodToGive = 3;
    float woodPickupPopForce = 50f;

    public void Interact(CharacterInteraction character)
    {
        // Set player to start chopping wood, on complete, call Chop()-
    }

    public void SetHighlight(bool value)
    {
        //TODO: Get an object highlight shader
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Chop()
    {
        for (int i = 0; i < numWoodToGive; i++)
        {
            GameObject woodPickup = Instantiate(woodPickupPrefab, transform.position, Quaternion.identity);
            float angle = (360f / numWoodToGive) * i;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = Mathf.Sin(Mathf.Deg2Rad * angle);
            float z = 0;

            Vector3 pushVector = new Vector3(x, y, z);
        }

        DestroyDualObject();
    }

    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }
}
