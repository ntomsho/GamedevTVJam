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
            natureCanvasGroup.alpha = 1;
            techCanvasGroup.alpha = 0;
        } else
        {
            natureCanvasGroup.alpha = 0;
            techCanvasGroup.alpha = 1;
        }
    }

    void ClearUI()
    {
        natureCanvasGroup.alpha = 0;
        techCanvasGroup.alpha = 0;
    }
}
