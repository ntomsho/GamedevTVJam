using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour, IInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] Outline highlightOutline;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject goodsPickupPrefab;
    [SerializeField] GameObject electronicsPickupPrefab;
    [SerializeField] float interactDuration = 1f;
    [SerializeField] float timeToGrow = 15f;
    [SerializeField] int maxGoods = 3;
    bool isPowered = false;
    bool makingElectronics = false;
    int numGoods = 0;
    float growthTimer = 0f;
    WorldType worldType = WorldType.Technology;
    [SerializeField] int electronicsSiliconCost = 2;
    ResourceCost[] siliconCost;
    
    void Start()
    {
        siliconCost = new ResourceCost[1] { new ResourceCost(ResourceType.Silicon, electronicsSiliconCost) };
    }

    // TODO: Collisions + events to determine if powered or not

    public float GetTimeToInteract()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(CharacterInteraction character)
    {
        if (numGoods == 0)
        {
            if (character.gameObject.GetComponent<Inventory>().TryToSpendResources(siliconCost))
            {
                makingElectronics = true;
            }
        } else
        {
            CreateGoods();
        }
    }

    void CreateGoods()
    {
        numGoods--;
        Instantiate(goodsPickupPrefab, transform.position, Quaternion.identity);
    }

    void CreateElectronics()
    {
        Instantiate(electronicsPickupPrefab, transform.position, Quaternion.identity);
    }

    public void SetHighlight(bool value)
    {
        
    }
    
    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    public bool GetIsGrown()
    {
        return numGoods > 0;
    }

    public float GetTimeToGrow()
    {
        return timeToGrow;
    }

    public void Grow()
    {
        numGoods++;
        growthTimer = 0f;
    }

    void Update()
    {
        if (makingElectronics || numGoods < maxGoods)
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= timeToGrow)
            {
                if (makingElectronics)
                {
                    CreateElectronics();
                } else
                {
                    Grow();
                }
            }
        }
    }
}
