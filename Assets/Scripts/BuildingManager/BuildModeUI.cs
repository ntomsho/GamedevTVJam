using System;
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
        WorldSwap.Instance.OnWorldSwap += OnWorldSwap;
    }

    void OnWorldSwap(object sender, EventArgs e)
    {
        EnableUI(WorldSwap.Instance.GetIsInNatureWorld());
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
            natureCanvasGroup.alpha = 1;
            techCanvasGroup.alpha = 0;
            natureCanvasGroup.gameObject.SetActive(true);
            techCanvasGroup.gameObject.SetActive(false);
        } else
        {
            natureCanvasGroup.alpha = 0;
            techCanvasGroup.alpha = 1;
            natureCanvasGroup.gameObject.SetActive(false);
            techCanvasGroup.gameObject.SetActive(true);
        }
    }

    void ClearUI()
    {
        natureCanvasGroup.alpha = 0;
        techCanvasGroup.alpha = 0;
        natureCanvasGroup.gameObject.SetActive(false);
        techCanvasGroup.gameObject.SetActive(false);
    }
}
