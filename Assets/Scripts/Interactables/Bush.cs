using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : BaseInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject seedsPickupPrefab;
    [SerializeField] float timeToGrow = 30f;
    
    bool isGrown = false;
    float growthTimer = 0f;
    WorldType worldType = WorldType.Nature;
    private Vector3 grownScale;

    private void Start()
    {
        grownScale = transform.localScale;
    }


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
            transform.localScale = grownScale * (growthTimer / timeToGrow);
            growthTimer += Time.deltaTime;
            if (growthTimer >= timeToGrow) Grow();
        }
    }
}
