using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{

    public float xLimit = 10f;
    public float xLimitMinus = 10f;
    public float yLimit = 10f;
    public float yLimitMinus = 10f;

    public Transform map;

    private void Start()
    {



    }

    void Update()
    {
        //transform.LookAt(map); if we need to lock camera on map?

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position;
        newPosition.x += horizontalInput;
        newPosition.z += verticalInput;

        newPosition.x = Mathf.Clamp(newPosition.x , -xLimitMinus , xLimit);
        newPosition.z = Mathf.Clamp(newPosition.z , -yLimitMinus , yLimit);

        transform.position = newPosition;
        // Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;
    }
}
