using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualObject : MonoBehaviour
{
    [SerializeField] GameObject natureWorldObject;
    [SerializeField] GameObject techWorldObject;

    void Start()
    {
        SetGameObjectsActive();
    }

    bool GetIsInNatureWorld()
    {
        return WorldSwap.Instance.GetIsInNatureWorld();
    }

    public void SetGameObjectsActive()
    {
        if (GetIsInNatureWorld())
        {
            natureWorldObject.SetActive(true);
            techWorldObject.SetActive(false);
        } else
        {
            natureWorldObject.SetActive(false);
            techWorldObject.SetActive(true);
        }
    }
}
