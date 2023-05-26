using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : BaseInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] Outline highlightOutline;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject seedsPickupPrefab;
    [SerializeField] float interactDuration = 2f;
    [SerializeField] float timeToGrow = 30f;
    bool isGrown = false;
    float growthTimer = 0f;
    WorldType worldType = WorldType.Nature;

    public override void Interact(CharacterInteraction character)
    {
        GatherSeeds();
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void GatherSeeds()
    {
        if (!GetIsGrown()) return;
        Instantiate(seedsPickupPrefab, transform.position, Quaternion.identity);
        isGrown = false;
    }

    public WorldType GetWorldType()
    {
        return worldType;
    }

    public float GetTimeToGrow()
    {
        return timeToGrow;
    }

    public bool GetIsGrown()
    {
        return isGrown;
    }

    public void Grow()
    {
        // Change model/scale
        isGrown = true;
        growthTimer = 0f;
    }

    public void DestroyDualObject()
    {
        return; //Can't be destroyed;
    }

    void Update()
    {
        if (!GetIsGrown())
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= timeToGrow) Grow();
        }
    }
}