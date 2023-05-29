using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour
{
    public static event EventHandler OnAnyBuildingPlaced;
    public static event EventHandler OnAnyNatureSelected;
    public static event EventHandler OnAnyTechSelected;

    [SerializeField] Inventory playerInventory;
    public GameObject[] objects;
    [SerializeField] public List<Buildable> buildablesListNature;
    [SerializeField] public List<Buildable> buildablesListTech;

    public Camera topDownCamera;
    public float rotateAmount;
    public float gridSize;
    public bool canPlace = true;
    private bool gridOn = true;

    //TODO: Make these two private
    public Buildable pendingBuildable;
    public GameObject pendingObject;

    private RaycastHit hit;
    private Vector3 buildablePosition;
    [SerializeField] private LayerMask buildableLayer;
    [SerializeField] private Toggle gridToggle;
    [SerializeField] private Material canBuildMaterial;
    [SerializeField] private Material cantBuildMaterial;

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.OnBuildModeChanged += OnBuildModeChanged;
    }

    // Update is called once per frame
    private void Update()
    {
        if (pendingObject != null)
        {
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(buildablePosition.x) ,
                    RoundToNearestGrid(buildablePosition.y) ,
                    RoundToNearestGrid(buildablePosition.z));
            }
            else { pendingObject.transform.position = buildablePosition; }
            
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0)&&canPlace)
                {
                    PlaceObject();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                ClearSelection();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            
            CheckCanPlace();
            UpdateMaterials();
        }
    }

    void CheckCanPlace()
    {
        Dictionary<ResourceType, int> resourceCost = new Dictionary<ResourceType, int>();
        foreach (ResourceCost cost in pendingBuildable.resourceCosts)
        {
            resourceCost.Add(cost.resourceType, cost.value);
        }

        if (!playerInventory.CanAffordResources(resourceCost))
        {
            canPlace = false;
            return;
        }

        //TODO: raycast to see if space is occuped;

        canPlace = true;
    }

    private void FixedUpdate()
    {
        Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray , out hit , 1000 , buildableLayer))
        {
            buildablePosition = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        ClearSelection();
        if (WorldSwap.Instance.GetIsInNatureWorld())
        {
            pendingBuildable =  buildablesListNature[index];

            OnAnyNatureSelected?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            pendingBuildable = buildablesListTech[index];

            OnAnyTechSelected?.Invoke(this, EventArgs.Empty);
        }
        pendingObject = Instantiate(pendingBuildable.previewPrefab, buildablePosition, transform.rotation);
        // SetSelected Event
    }

    void OnBuildModeChanged(object sender, bool value)
    {
        if (!value) ClearSelection();
    }

    public void ClearSelection()
    {
        pendingBuildable = null;
        if (pendingObject)
        {
            Destroy(pendingObject);
            pendingObject = null;
        }
    }

    private void PlaceObject()
    {
        if (pendingBuildable.Build(pendingObject.transform.position, pendingObject.transform.rotation, playerInventory))
        {
            Destroy(pendingObject);
            pendingObject = null;
            pendingBuildable = null;
            OnAnyBuildingPlaced?.Invoke(this, EventArgs.Empty);
        } else
        {
            //Negative feedback
        }
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else { gridOn = false; }
    }
    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;

        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }

        if (xDiff * (-1f) > (gridSize / 2))
        {
            pos -= gridSize;
        }

        return pos;
    }
    void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up , rotateAmount);
    }
    void UpdateMaterials()
    {
        if (pendingObject == null) return;
        if (!canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = cantBuildMaterial;
        }
        else
        {
            pendingObject.GetComponent<MeshRenderer>().material = canBuildMaterial;
        }
    }
}