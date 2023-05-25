using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeUI : MonoBehaviour
{
    [SerializeField] CanvasGroup natureCanvasGroup;
    [SerializeField] CanvasGroup techCanvasGroup;

    void Awake()
    {
        ClearUI();
    }

    void Start()
    {
        GameManager.Instance.OnBuildModeChanged += UpdateBuildMode;
    }

    void UpdateBuildMode(object sender, bool value)
    {
        if (value)
        {
            EnableUI(WorldSwap.Instance.GetIsInNatureWorld());
        } else
        {
            ClearUI();
        }
    }

    void EnableUI(bool isInNatureWorld)
    {
        if (isInNatureWorld)
        {
            natureCanvasGroup.enabled = true;
            techCanvasGroup.enabled = false;
        } else
        {
            natureCanvasGroup.enabled = false;
            techCanvasGroup.enabled = true;
        }
    }

    void ClearUI()
    {
        natureCanvasGroup.enabled = false;
        techCanvasGroup.enabled = false;
    }
}
