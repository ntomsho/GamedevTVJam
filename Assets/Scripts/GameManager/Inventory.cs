using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ResourceType
{
    Fish, Seeds, Gravel, Silicon, Wood, Fruit, Steel, Grain, Goods, Electronics
}

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI resources;
    int numFish;
    int numSeeds = 3;
    int numGravel;
    int numSilicon;
    int numWood;
    int numFruit;
    int numSteel;
    int numGrain;
    int numGoods;
    int numElectronics;

    void UpdateUI()
    {
        resources.text = "Fish: " + GetNumResource(ResourceType.Fish).ToString() + "\n" +
             "Seeds: " + GetNumResource(ResourceType.Seeds).ToString() + "\n" +
             "Gravel: " + GetNumResource(ResourceType.Gravel).ToString() + "\n" +
             "Silicon: " + GetNumResource(ResourceType.Silicon).ToString() + "\n" +
             "Wood: " + GetNumResource(ResourceType.Wood).ToString() + "\n" +
             "Fruit: " + GetNumResource(ResourceType.Fruit).ToString() + "\n" +
             "Steel: " + GetNumResource(ResourceType.Steel).ToString() + "\n" +
             "Grain: " + GetNumResource(ResourceType.Grain).ToString() + "\n" +
             "Goods: " + GetNumResource(ResourceType.Goods).ToString() + "\n" +
             "Electronics: " + GetNumResource(ResourceType.Electronics).ToString();
    }

    public static bool IsLuxuryResource(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Fish:
            case ResourceType.Fruit:
            case ResourceType.Goods:
            case ResourceType.Electronics:
                return true;
            default:
                return false;
        }
    }

    public int GetNumResource(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Fish: return numFish;
            case ResourceType.Seeds: return numSeeds;
            case ResourceType.Gravel: return numGravel;
            case ResourceType.Silicon: return numSilicon;
            case ResourceType.Wood: return numWood;
            case ResourceType.Fruit: return numFruit;
            case ResourceType.Steel: return numSteel;
            case ResourceType.Grain: return numGrain;
            case ResourceType.Goods: return numGoods;
            case ResourceType.Electronics: return numElectronics;
            default: return 0;
        }
    }

    public void AddResource(ResourceType resourceType, int numToAdd)
    {
        switch (resourceType)
        {
            case ResourceType.Fish: numFish += numToAdd; break;
            case ResourceType.Seeds: numSeeds += numToAdd; break;
            case ResourceType.Gravel: numGravel += numToAdd; break;
            case ResourceType.Silicon: numSilicon += numToAdd; break;
            case ResourceType.Wood: numWood += numToAdd; break;
            case ResourceType.Fruit: numFruit += numToAdd; break;
            case ResourceType.Steel: numSteel += numToAdd; break;
            case ResourceType.Grain: numGrain += numToAdd; break;
            case ResourceType.Goods: numGoods += numToAdd; break;
            case ResourceType.Electronics: numElectronics += numToAdd; break;
        }
        UpdateUI();
    }

    public bool CanAffordResources(Dictionary<ResourceType, int> totalCosts)
    {
        foreach(KeyValuePair<ResourceType, int> entry in totalCosts)
        {
            switch(entry.Key)
            {
                case ResourceType.Fish: if (numFish < entry.Value) return false; break;
                case ResourceType.Seeds: if (numSeeds < entry.Value) return false; break;
                case ResourceType.Gravel: if (numGravel < entry.Value) return false; break;
                case ResourceType.Silicon: if (numSilicon < entry.Value) return false; break;
                case ResourceType.Wood: if (numWood < entry.Value) return false; break;
                case ResourceType.Fruit: if (numFruit < entry.Value) return false; break;
                case ResourceType.Steel: if (numSteel < entry.Value) return false; break;
                case ResourceType.Grain: if (numGrain < entry.Value) return false; break;
                case ResourceType.Goods: if (numGoods < entry.Value) return false; break;
                case ResourceType.Electronics: if (numElectronics < entry.Value) return false; break;
            }
        }
        return true;
    }

    public bool TryToSpendResources(ResourceCost[] resourceCosts)
    {
        Dictionary<ResourceType, int> totalCosts = new Dictionary<ResourceType, int>();
        foreach (ResourceCost cost in resourceCosts)
        {
            if (totalCosts.ContainsKey(cost.resourceType))
            {
                totalCosts[cost.resourceType] += cost.value;
            } else
            {
                totalCosts.Add(cost.resourceType, cost.value);
            }
        }

        // TODO: Improve this
        if (!CanAffordResources(totalCosts)) return false;

        foreach(KeyValuePair<ResourceType, int> entry in totalCosts)
        {
            switch (entry.Key)
            {
                case ResourceType.Fish: numFish -= entry.Value; break;
                case ResourceType.Seeds: numSeeds -= entry.Value; break;
                case ResourceType.Gravel: numGravel -= entry.Value; break;
                case ResourceType.Silicon: numSilicon -= entry.Value; break;
                case ResourceType.Wood: numWood -= entry.Value; break;
                case ResourceType.Fruit: numFruit -= entry.Value; break;
                case ResourceType.Steel: numSteel -= entry.Value; break;
                case ResourceType.Grain: numGrain -= entry.Value; break;
                case ResourceType.Goods: numGoods -= entry.Value; break;
                case ResourceType.Electronics: numElectronics -= entry.Value; break;
            }
        }
        UpdateUI();
        return true;
    }
}
