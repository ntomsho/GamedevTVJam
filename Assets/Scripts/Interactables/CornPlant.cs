using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornPlant : MonoBehaviour, IInteractable, IGrowOverTime
{
    [SerializeField] GameObject growthLevel1;
    [SerializeField] GameObject growthLevel2;
    [SerializeField] GameObject growthLevel3;
    [SerializeField] GameObject grainPickupPrefab;
    int growthLevel = 1;

    [SerializeField] float timeToHarvest = 1.5f;

    Field fieldParent;

    void Start()
    {
        growthLevel2.SetActive(false);
        growthLevel3.SetActive(false);
    }

    public void Grow()
    {
        if (growthLevel < 3)
        {
            growthLevel++;
        }
        if (growthLevel == 2)
        {
            growthLevel1.SetActive(false);
            growthLevel2.SetActive(true);
        } else if (growthLevel == 3)
        {
            growthLevel2.SetActive(false);
            growthLevel3.SetActive(true);
        }
    }

    public float GetTimeToInteract()
    {
        return timeToHarvest;
    }

    public void Interact(CharacterInteraction character)
    {
        Instantiate(grainPickupPrefab, transform.position, Quaternion.identity);
        fieldParent.DestroyPlant(this);
        Destroy(gameObject);
    }

    public void SetHighlight(bool value)
    {
        
    }

    public bool GetIsGrown()
    {
        return growthLevel == 3;
    }

    public float GetTimeToGrow()
    {
        return 0f;
    }

    public void SetFieldParent(Field field)
    {
        fieldParent = field;
    }
}
