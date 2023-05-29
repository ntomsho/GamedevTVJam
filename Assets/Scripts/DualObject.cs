using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualObject : MonoBehaviour
{
    public  event EventHandler OnDualObjectDestroyed;

    [SerializeField] GameObject natureWorldObjectContainer;
    [SerializeField] GameObject techWorldObjectContainer;

    protected GameObject natureWorldObject;
    protected GameObject techWorldObject;

    Renderer natureWorldRenderer;
    Renderer techWorldRenderer;

    void Awake()
    {
        natureWorldObject = natureWorldObjectContainer.transform.GetChild(0).gameObject;
        techWorldObject = techWorldObjectContainer.transform.GetChild(0).gameObject;
        natureWorldRenderer = natureWorldObject.GetComponent<Renderer>();
        techWorldRenderer = techWorldObject.GetComponent<Renderer>();
    }

    void Start()
    {
        SetGameObjectsActive();
    }

    bool GetIsInNatureWorld()
    {
        return WorldSwap.Instance.GetIsInNatureWorld();
    }

    public GameObject GetNatureWorldObject()
    {
        return natureWorldObject;
    }

    public GameObject GetTechWorldObject()
    {
        return techWorldObject;
    }

    public virtual void SwapObjectOpacity(float materialChangeDuration)
    {
        natureWorldObject.SetActive(true);
        techWorldObject.SetActive(true);

        Material oldMaterial = !GetIsInNatureWorld() ? natureWorldRenderer.material : techWorldRenderer.material;
        Material newMaterial = !GetIsInNatureWorld() ? techWorldRenderer.material : natureWorldRenderer.material;

        StartCoroutine(HandleObjectOpacityLerp(oldMaterial, newMaterial, materialChangeDuration));
    }

    IEnumerator HandleObjectOpacityLerp(Material oldMaterial, Material newMaterial, float materialChangeDuration)
    {
        float timeElapsed = 0;
        Color oldMaterialColor = oldMaterial.color;
        Color newMaterialColor = newMaterial.color;

        while (timeElapsed < materialChangeDuration)
        {
            oldMaterial.color = new Color(oldMaterialColor.r, oldMaterialColor.g, oldMaterialColor.b, 1f - (timeElapsed / materialChangeDuration));
            newMaterial.color = new Color(newMaterialColor.r, newMaterialColor.g, newMaterialColor.b, (timeElapsed / materialChangeDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // oldMaterial.color = new Color(oldMaterialColor.r, oldMaterialColor.g, oldMaterialColor.b, 0f);
        // newMaterial.color = new Color(newMaterialColor.r, newMaterialColor.g, newMaterialColor.b, 1f);

        SetGameObjectsActive();
    }

    public virtual void SetGameObjectsActive()
    {
        if (GetIsInNatureWorld())
        {
            natureWorldObject.SetActive(true);
            techWorldObject.SetActive(false);
            natureWorldRenderer.material.color = new Color(natureWorldRenderer.material.color.r, natureWorldRenderer.material.color.g, natureWorldRenderer.material.color.b, 1f);
            techWorldRenderer.material.color = new Color(techWorldRenderer.material.color.r, techWorldRenderer.material.color.g, techWorldRenderer.material.color.b, 0f);
        } else
        {
            natureWorldObject.SetActive(false);
            techWorldObject.SetActive(true);
            natureWorldRenderer.material.color = new Color(natureWorldRenderer.material.color.r, natureWorldRenderer.material.color.g, natureWorldRenderer.material.color.b, 0f);
            techWorldRenderer.material.color = new Color(techWorldRenderer.material.color.r, techWorldRenderer.material.color.g, techWorldRenderer.material.color.b, 1f);
        }
    }

    public void DestroyDualObject()
    {
        OnDualObjectDestroyed?.Invoke(this, EventArgs.Empty);
        // Manage changes to harmony;
        // Play destroy animation;
        Destroy(gameObject);
    }
}
