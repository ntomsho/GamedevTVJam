using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwap : MonoBehaviour
{
    public static WorldSwap Instance { get; private set; }

    bool isInNatureWorld = true;
    [SerializeField] List<Renderer> renderers;
    [SerializeField] List<Material> natureWorldMaterials;
    [SerializeField] List<Material> techWorldMaterials;
    List<DualObject> dualObjects = new List<DualObject>();

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one WorldSwap - " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SwapWorld()
    {
        isInNatureWorld = !isInNatureWorld;
        CreateTransitionOverlay();
        SwapMeshRendererMaterials();
    }

    void CreateTransitionOverlay()
    {
        // TODO: Implement transition effect
    }

    void SwapMeshRendererMaterials()
    {
        foreach (Renderer renderer in renderers)
        {
            List<Material> oldList = isInNatureWorld ? techWorldMaterials : natureWorldMaterials;
            List<Material> newList = isInNatureWorld ? natureWorldMaterials : techWorldMaterials;
            int meshIndex = oldList.FindIndex(material => material);
            if (meshIndex == -1)
            {
                Debug.Log($"Error: Texture {renderer.material} not found");
                return;
            }
            // TODO: Turn this into a crossfade effect
            Debug.Log(renderer.material);
        }
    }

    public void AddToDualObjectsList(DualObject newObject)
    {
        dualObjects.Add(newObject);
    }

    public void RemoveFromDualObjectsList(DualObject objectToRemove)
    {
        dualObjects.Remove(objectToRemove);
    }

    public bool GetIsInNatureWorld()
    {
        return isInNatureWorld;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapWorld();
        }
    }
}
