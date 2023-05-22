using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Fish, Seeds, Gravel, Silicon, Wood, Fruit, Steel, Grain, Goods, Electronics
}

public class Inventory : MonoBehaviour
{
    int numFish;
    int numSeeds;
    int numGravel;
    int numSilicon;
    int numWood;
    int numFruit;
    int numSteel;
    int numGrain;
    int numGoods;
    int numElectronics;

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
    }
}
