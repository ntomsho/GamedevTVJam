using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ResourceCost
{
    public ResourceType resourceType;
    public int value;
    public ResourceCost(ResourceType resourceType, int value)
    {
        this.resourceType = resourceType;
        this.value = value;
    }
}

public enum WorldType
{
    Nature,
    Technology,
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BuildableScriptableObject", order = 1)]
public class Buildable : ScriptableObject
{
    public ResourceCost[] resourceCosts;
    public WorldType worldType;
    public GameObject dualObjectPrefab;
    public GameObject previewPrefab;
    
    public bool Build(Vector3 buildPoint, Quaternion buildRotation, Inventory playerInventory)
    {
        if (playerInventory.TryToSpendResources(resourceCosts))
        {
            Instantiate(dualObjectPrefab, buildPoint, buildRotation);
            return true;
        }
        return false;
    }
}
