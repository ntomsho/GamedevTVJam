using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class WorldSwap : MonoBehaviour
{
    public static WorldSwap Instance { get; private set; }

    bool swapInProgress;
    bool isInNatureWorld = true;
    [SerializeField] List<Renderer> renderers;
    [SerializeField] List<Material> natureWorldMaterials;
    [SerializeField] List<Material> techWorldMaterials;
    [SerializeField] float materialChangeDuration = 1.5f;
    [SerializeField] Volume volume;
    UnityEngine.Rendering.Universal.Bloom bloom;
    List<DualObject> dualObjects = new List<DualObject>();

    [SerializeField] DualObject testDualObject;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one WorldSwap - " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        AddToDualObjectsList(testDualObject);
        Debug.Log(dualObjects[0]);
    }
    
    void Start()
    {
        volume.profile.TryGet(out bloom);
    }

    public void SwapWorld()
    {
        isInNatureWorld = !isInNatureWorld;
        swapInProgress = true;
        StartCoroutine(HandleBloom());
        SwapMeshRendererMaterials();
        SwapDualObjects();
    }

    IEnumerator HandleBloom()
    {
        while (bloom.threshold.value > 0)
        {
            bloom.threshold.value -= 0.01f;
            bloom.intensity.value += 0.1f;
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);

        while (bloom.threshold.value < 1f)
        {
            bloom.threshold.value += 0.01f;
            bloom.intensity.value -= 0.1f;
            yield return null;
        }
        swapInProgress = false;
    }

    IEnumerator HandleMaterialLerp(Material material1, Material material2, Renderer renderer)
    {
        float timeElapsed = 0;
        while (timeElapsed < materialChangeDuration)
        {
            renderer.material.Lerp(material1, material2, timeElapsed / materialChangeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        renderer.material = material2;
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
            StartCoroutine(HandleMaterialLerp(oldList[meshIndex], newList[meshIndex], renderer));
        }
    }

    void SwapDualObjects()
    {
        foreach (DualObject dualObject in dualObjects)
        {
            dualObject.SwapObjectOpacity(materialChangeDuration);
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

    public bool GetIsSwapInProgress()
    {
        return swapInProgress;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Swap started");
            SwapWorld();
        }
    }
}
