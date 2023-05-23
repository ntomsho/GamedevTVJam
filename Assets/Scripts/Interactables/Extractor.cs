using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour, IInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] ResourceCost[] resourceCosts;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject steelPickupPrefab;
    [SerializeField] float interactDuration = 2f;
    [SerializeField] float timeToGenerateSteel = 15f;
    [SerializeField] int maxAmountOfSteel = 5;
    int amountOfSteel = 0;
    WorldType worldType = WorldType.Technology;

    public void Interact(CharacterInteraction character)
    {
        // Set player animation, on complete call Extract()
    }

    public float GetTimeToInteract()
    {
        return interactDuration;
    }

    public void SetHighlight(bool value)
    {
        //TODO: Get an object highlight shader
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Extract()
    {
        GameObject steelPickup = Instantiate(steelPickupPrefab, transform.position, Quaternion.identity);
        // Shoot out the object to a random location nearby;
        amountOfSteel -= 1;
    }

    public WorldType GetWorldType()
    {
        return worldType;
    }

    public ResourceCost[] GetResourceCost()
    {
        return resourceCosts;
    }

    public float GetTimeToGrow()
    {
        return timeToGenerateSteel;
    }

    public void Grow()
    {
        if (amountOfSteel < maxAmountOfSteel) amountOfSteel++;
    }

    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }
}
