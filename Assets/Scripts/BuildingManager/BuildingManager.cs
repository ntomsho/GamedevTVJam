using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject pendingObject;
    public Camera main;
    private Vector3 buildablePosition;


    private RaycastHit hit;
    [SerializeField] private LayerMask buildableLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = buildablePosition;
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }
    private void FixedUpdate()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray , out hit , 1000 , buildableLayer))
        {
            buildablePosition = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index] , buildablePosition , transform.rotation);
    }
    void PlaceObject()
    {
        pendingObject = null;
    }
}
