using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject player;
    public GameObject topDownCamera;
    public Camera playerCamera;
    public Camera topDowncamera;

    [SerializeField] Cinemachine.CinemachineVirtualCamera playerVCam;
    // Start is called before the first frame update
    void Start()
    {
        // playerCamera.enabled = true;
        // topDowncamera.enabled = false;

        GameManager.Instance.OnBuildModeChanged += UpdateBuildMode;
    }

    void UpdateBuildMode(object sender, bool value)
    {
        // playerCamera.enabled = !value;
        // topDowncamera.enabled = value;
        playerVCam.m_Priority = value ? 5 : 11;

        player.SetActive(!value);
        topDownCamera.SetActive(value);
    }
}
