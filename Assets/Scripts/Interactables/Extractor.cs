using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : BaseInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] ResourceCost[] resourceCosts;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject steelPickupPrefab;
    [SerializeField] float timeToGenerateSteel = 20f;
    [SerializeField] int maxAmountOfSteel = 5;
    [SerializeField] Transform dropSpot;
    int amountOfSteel = 0;
    float growthTimer = 0f;
    WorldType worldType = WorldType.Technology;

    public override void Interact(CharacterInteraction character)
    {
        // Set player animation, on complete call Extract()
        Extract();
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Extract()
    {
        if (!GetIsGrown()) return;

        SoundManager.Instance.PlayUICraftSound(PlayerManager.Instance.transform.position);

        GameObject steelPickup = Instantiate(steelPickupPrefab, dropSpot.position, Quaternion.identity);
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
        amountOfSteel++;
        growthTimer = 0f;
    }

    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }

    public bool GetIsGrown()
    {
        return amountOfSteel > 0;
    }

    void Update()
    {
        if (amountOfSteel < maxAmountOfSteel)
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= timeToGenerateSteel) Grow();
        }
    }
}
