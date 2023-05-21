using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject player;
    public GameObject topDownCamera;
    public Camera playerCamera;
    public Camera topDowncamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera.enabled = true;
        topDowncamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerCamera.enabled = !playerCamera.enabled;
            topDowncamera.enabled = !topDowncamera.enabled;
        }
        if (topDowncamera.enabled)
        {
            player.SetActive(false);
            topDownCamera.SetActive(true);
        }
        else
        {
            player.SetActive(true);
            topDownCamera.SetActive(false);
        }
    }
}
