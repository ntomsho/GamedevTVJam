using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : BaseInteractable, IDualObjectChild, IGrowOverTime
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] Animation playerAnimation;
    [SerializeField] SphereCollider waterCheckCollider;
    [SerializeField] GameObject cornPlantPrefab;
    [SerializeField] GameObject grainPickupPrefab;
    [SerializeField] GameObject fruitPickupPrefab;
    [SerializeField] Transform resourceDropPosition;
    [SerializeField] float timeToInteract = 2f;
    [SerializeField] float timeToGrow = 10f;
    float growthTimer = 0f;
    WorldType worldType = WorldType.Nature;
    [SerializeField] List<CornPlant> plantsList;
    bool isNearWater;

    void Awake()
    {
        plantsList = new List<CornPlant>();
    }

    void Start()
    {
        CheckForWater();
        CreateNewPlant();
    }

    public override void Interact(CharacterInteraction character)
    {
        Instantiate(isNearWater ? fruitPickupPrefab : grainPickupPrefab, resourceDropPosition.position, Quaternion.identity);
        DestroyPlant(plantsList[0]);
    }

    void CheckForWater()
    {
        GameObject water = GameObject.FindGameObjectWithTag("Water");
        if (waterCheckCollider.bounds.Contains(water.transform.position))
        {
            isNearWater = true;
            return;
        }
        isNearWater = false;
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void CreateNewPlant()
    {
        Debug.Log("creating plant");
        int spawnIndex = 0;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i].childCount == 0)
            {
                spawnIndex = i;
                break;
            }
        }

        Transform spawnPoint = spawnPoints[spawnIndex];
        GameObject newPlantGameObject = Instantiate(cornPlantPrefab , spawnPoint.position , Quaternion.identity, spawnPoint);//
        CornPlant newPlant = newPlantGameObject.GetComponent<CornPlant>();
        plantsList.Add(newPlant);
    }

    public void DestroyPlant(CornPlant plant)
    {
        Destroy(plant.gameObject);
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
                CreateNewPlant();
            }
            else
            {
                currentPlant.Grow();
            }
        }
        growthTimer = 0f;
    }

    public bool GetIsGrown()
    {
        foreach(CornPlant plant in plantsList)
        {
            if (plant.GetIsGrown()) return true;
        }
        return false;
    }

    public override float GetTimeToInteract()
    {
        return timeToInteract;
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
