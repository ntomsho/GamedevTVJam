using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : BaseInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;

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

    public override void Interact(CharacterInteraction character)
    {
        return; //No interaction
    }

    // TODO: Collision + event to determine if close to water

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
        if (plantsList != null && plantsList.Count > 0)
        {
            CornPlant currentPlant = plantsList[plantsList.Count - 1];
            if (currentPlant == null) return;
            if (currentPlant.GetIsGrown())
            {
                
            }
            else
            {
                currentPlant.Grow();
            }
        }   
    }

    public bool GetIsGrown()
    {
        return false;
    }

    public override float GetTimeToInteract()
    {
        return 0;
    }

    public override void SetHighlight(bool value)
    {
        return;
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
