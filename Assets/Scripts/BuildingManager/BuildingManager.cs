using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    [SerializeField] public List<Buildable> buildablesList;
    [SerializeField] private Buildable[] buildables;

    public Camera topDownCamera;
    public float rotateAmount;
    public float gridSize;
    public bool canPlace = true;
    private bool gridOn = true;
    public GameObject pendingObject;
    private RaycastHit hit;
    private Vector3 buildablePosition;
    [SerializeField] private LayerMask buildableLayer;
    [SerializeField] private Toggle gridToggle;
    [SerializeField] private Material[] materials;

    // Start is called before the first frame update
    private void Start()
    {
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
            if (Input.GetMouseButtonDown(0)&&canPlace)
            {
                PlaceObject();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            UpdateMaterials();
        }

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
        pendingObject = Instantiate(buildables[index].previewPrefab, buildablePosition , transform.rotation);
    }

    private void PlaceObject()
    {
        pendingObject.GetComponent<MeshRenderer>().material = materials[2];
        pendingObject = null;

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
        if (canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        else
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }
}