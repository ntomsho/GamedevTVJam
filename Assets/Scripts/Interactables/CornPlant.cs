using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornPlant : MonoBehaviour, IGrowOverTime
{
    [SerializeField] GameObject growthLevel1;
    [SerializeField] GameObject growthLevel2;
    [SerializeField] GameObject growthLevel3;
    // [SerializeField] GameObject grainPickupPrefab;
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
            Debug.Log($"Growing to level {growthLevel}");
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

    // public override void Interact(CharacterInteraction character)
    // {
    //     Instantiate(grainPickupPrefab, transform.position, Quaternion.identity);
    //     fieldParent.DestroyPlant(this);
    //     Destroy(gameObject);
    // }

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
