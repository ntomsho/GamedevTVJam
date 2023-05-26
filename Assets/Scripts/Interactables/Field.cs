using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour, IInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] Outline highlightOutline;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject cornPlantPrefab;
    [SerializeField] float timeToGrow = 10f;
    [SerializeField] int maxPlants = 6;
    float growthTimer = 0f;
    WorldType worldType = WorldType.Nature;
    List<CornPlant> plantsList;

    void Awake()
    {
        plantsList = new List<CornPlant>();
    }

    void Start()
    {
        CreateNewPlant();
    }

    public void Interact(CharacterInteraction character)
    {
        return; //No interaction
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void CreateNewPlant()
    {
        // Get position;
        GameObject newPlantGameObject = Instantiate(cornPlantPrefab, transform.position, Quaternion.identity);
        CornPlant newPlant = newPlantGameObject.GetComponent<CornPlant>();
        plantsList.Add(newPlant);
    }

    public void DestroyPlant(CornPlant plant)
    {
        plantsList.Remove(plant);
    }

    public void Grow()
    {
        CornPlant currentPlant = plantsList[plantsList.Count - 1];
        if (currentPlant.GetIsGrown())
        {

        } else
        {
            currentPlant.Grow();
        }
    }

    public bool GetIsGrown()
    {
        return false;
    }

    public float GetTimeToInteract()
    {
        throw new System.NotImplementedException();
    }

    public void SetHighlight(bool value)
    {
        throw new System.NotImplementedException();
    }

    public float GetTimeToGrow()
    {
        return timeToGrow;
    }

    public void DestroyDualObject()
    {
        dualObjectParent.DestroyDualObject();
    }
    
    void Update()
    {
        if (plantsList.Count < 6 || (plantsList.Count == 6 && !plantsList[5].GetIsGrown()))
        {
            growthTimer += Time.deltaTime;
            if (growthTimer >= timeToGrow) Grow();
        }
    }
}
