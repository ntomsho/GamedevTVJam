using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class WorldSwap : MonoBehaviour
{

    public event EventHandler OnWorldSwap;

    public static WorldSwap Instance { get; private set; }

    public event EventHandler OnWorldSwap;

    bool swapInProgress;
    bool isInNatureWorld = true;
    [SerializeField] Terrain terrain;
    [SerializeField] List<Renderer> renderers;
    [SerializeField] Texture2D[] natureWorldTextures2D;
    [SerializeField] Texture2D[] techWorldTextures2D;
    [SerializeField] Texture2D[] natureWorldTexturesNormal;
    [SerializeField] Texture2D[] techWorldTexturesNormal;
    [SerializeField] Material natureSkyboxMaterial;
    [SerializeField] Material techSkyboxMaterial;
    [SerializeField] MeshRenderer waterMesh;
    // [SerializeField] List<Material> natureWorldMaterials;
    // [SerializeField] List<Material> techWorldMaterials;
    [SerializeField] float materialChangeDuration = 1.5f;
    [SerializeField] Volume volume;
    UnityEngine.Rendering.Universal.Bloom bloom;

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
    
    void Start()
    {
        volume.profile.TryGet(out bloom);

        foreach (DualObject dualObject in FindObjectsByType<DualObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
        {
            AddToDualObjectsList(dualObject);
        }
    }

    public void SwapWorld()
    {
        isInNatureWorld = !isInNatureWorld;
        swapInProgress = true;
        StartCoroutine(HandleBloom());
        SwapTerrainRendererMaterials();
        SwapDualObjects();
        OnWorldSwap?.Invoke(this, EventArgs.Empty);
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

    void SwapTerrainRendererMaterials()
    {
        TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
        if (terrainLayers != null)
        {
            for (int index = 0; index < terrainLayers.Length; index++)
            {
                // TODO: Change to a crossfade
                terrainLayers[index].diffuseTexture = GetIsInNatureWorld() ? natureWorldTextures2D[index] : techWorldTextures2D[index];
                terrainLayers[index].normalMapTexture = GetIsInNatureWorld() ? natureWorldTexturesNormal[index] : techWorldTexturesNormal[index];
            }
        }
        RenderSettings.skybox = GetIsInNatureWorld() ? natureSkyboxMaterial : techSkyboxMaterial;
        waterMesh.enabled = GetIsInNatureWorld();
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Swap started");
            SwapWorld();
        }
    }
}
