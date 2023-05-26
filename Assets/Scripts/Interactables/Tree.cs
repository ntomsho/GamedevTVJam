using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] Outline highlightOutline;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject woodPickupPrefab;
    [SerializeField] float interactDuration = 2f;
    [SerializeField] float timeToGrow = 30f;
    [SerializeField] int maxWoodToGive = 3;
    float growthTimer = 0f;
    int numWoodToGive = 0;
    float woodPickupPopForce = 50f;
    WorldType worldType = WorldType.Nature;

    public void Interact(CharacterInteraction character)
    {
        // Set player to start chopping wood, on complete call Chop() if grown
        Chop();
    }

    public float GetTimeToInteract()
    {
        return interactDuration;
    }

    public void SetHighlight(bool value)
    {
        Debug.Log(value);
        highlightOutline.enabled = value;
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Chop()
    {
        if (!GetIsGrown()) return;
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
    }
}
