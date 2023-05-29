using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuilding : BaseInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;

    [SerializeField] Animation playerAnimation;

    [SerializeField] Transform questIndicatorPlaceholder;
    [SerializeField] GameObject questIndicator;
    [SerializeField] List<GameObject> resourceIndicators;
    [SerializeField] List<int> harmonyValues;
    [SerializeField] float timeToNewQuest = 60f;
    
    bool questActive;
    ResourceCost currentQuest;
    [SerializeField] WorldType worldType;
    float newQuestTimer;
    ResourceType[] luxuryResources;

    void Awake()
    {
        luxuryResources = new ResourceType[] {
            ResourceType.Fish, ResourceType.Electronics, ResourceType.Fruit, ResourceType.Goods
        };
    }

    public override void Interact(CharacterInteraction character)
    {
        if (!questActive) return;
        if (character.gameObject.GetComponent<Inventory>().TryToSpendResources(new ResourceCost[1] { currentQuest }))
        {
            CompleteQuest();    
        }
    }

    void CreateQuest()
    {
        System.Random random = new System.Random();
        int resourceIndex = random.Next(0, luxuryResources.Length);
        ResourceType resource = luxuryResources[resourceIndex];

        int value = 1;
        float randomValue = UnityEngine.Random.Range(0, 1f);
        if (randomValue > 0.6f) value = 2;
        else if (randomValue > 0.85f) value = 3;

        currentQuest = new ResourceCost(resource, value);
        questActive = true;
        questIndicator = Instantiate(resourceIndicators[resourceIndex], questIndicatorPlaceholder.position, Quaternion.identity, questIndicatorPlaceholder);
        newQuestTimer = 0f;
    }

    void CompleteQuest()
    {
        questActive = false;
        int natureHarmony = WorldSwap.Instance.GetIsInNatureWorld() ? harmonyValues[currentQuest.value - 1] : 0;
        int techHarmony = WorldSwap.Instance.GetIsInNatureWorld() ? 0 : harmonyValues[currentQuest.value - 1];
        GameManager.Instance.GainHarmony(natureHarmony, techHarmony);
        questIndicator.SetActive(false);
        Destroy(questIndicator);
        questIndicator = null;
    }

    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    public override bool GetIsQuestBuilding()
    {
        return true;
    }

    void Update()
    {
        if (questIndicator != null)
        {
            questIndicator.transform.Rotate(Vector3.up * 0.1f);
        }

        if (!questActive)
        {
            newQuestTimer += Time.deltaTime;
            if (newQuestTimer > timeToNewQuest)
            {
                CreateQuest();
            }
        }
    }
}
