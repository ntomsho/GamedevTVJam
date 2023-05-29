using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BaseInteractable, IInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject woodPickupPrefab;
    [SerializeField] float timeToGrow = 30f;
    [SerializeField] int maxWoodToGive = 3;
    [SerializeField] List<Transform> dropSpots;
    float growthTimer = 0f;
    int numWoodToGive = 0;
    float woodPickupPopForce = 50f;
    WorldType worldType = WorldType.Nature;

    public override void Interact(CharacterInteraction character)
    {
        // Set player to start chopping wood, on complete call Chop() if grown
        Chop();
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Chop()
    {
        if (!GetIsGrown()) return;

        SoundManager.Instance.PlayCraftSound(PlayerManager.Instance.transform.position);

        for (int i = 0; i < numWoodToGive; i++)
        {
            GameObject woodPickup = Instantiate(woodPickupPrefab, dropSpots[i].position, Quaternion.identity);
        }

        DestroyDualObject();
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
        return numWoodToGive > 0;
    }

    public void Grow()
    {
        // Change model/scale
        numWoodToGive++;
        growthTimer = 0f;
    }

    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }

    void Update()
    {
        if (numWoodToGive < maxWoodToGive)
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= timeToGrow) Grow();
        }
        if (!GetIsGrown())
        {
            transform.localScale = Vector3.one * (growthTimer / timeToGrow);
        }
    }
}
