using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectManager : MonoBehaviour
{
    public GameObject selectUI;
    public GameObject selectObject;
    public Camera topDownCamera;
    public TextMeshProUGUI objNameTxt;
    private BuildingManager buildingManager;
    // Start is called before the first frame update
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameIsInBuildMode) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.collider.gameObject.CompareTag("Object"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Deselect();
        }
    }
    void Select(GameObject obj)
    {
        if (obj == selectObject) return;
        if (selectObject != null) Deselect();
        // Outline outline = obj.GetComponent<Outline>();
        // if (outline == null) obj.AddComponent<Outline>();
        // else outline.enabled = true;
        objNameTxt.text = obj.name;
        selectObject = obj;
        selectUI.SetActive(true);
    }
    void Deselect()
    {
        // selectObject.GetComponent<Outline>().enabled = false;
        selectUI.SetActive(false);
        selectObject = null;
    }
    public void Delete()
    {
        GameObject objToDestroy = selectObject;
        Deselect();
        Destroy(objToDestroy);
    }
    public void Move()
    {
        buildingManager.pendingObject = selectObject;
    }
}
