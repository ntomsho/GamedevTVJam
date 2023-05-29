using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HarmonyPair
{
    public int natureHarmony;
    public int techHarmony;
    public HarmonyPair(int natureHarmony, int techHarmony)
    {
        this.natureHarmony = natureHarmony;
        this.techHarmony = techHarmony;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [SerializeField] int harmonyGoal = 2500;
    [SerializeField] CreditsHandler creditsHandler;
    public bool gameIsPaused = false;
    public bool gameIsInBuildMode = false;
    public GameObject pauseMenuUI;
    public event EventHandler<HarmonyPair> OnHarmonyChanged;
    public event EventHandler<bool> OnBuildModeChanged;
    public int natureHarmony;
    public int techHarmony;

    //public GameObject crosshair;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameManager - " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void GainHarmony(int natureHarmonyToAdd, int techHarmonyToAdd)
    {
        natureHarmony += natureHarmonyToAdd;
        techHarmony += techHarmonyToAdd;

        OnHarmonyChanged?.Invoke(this, new HarmonyPair(natureHarmony, techHarmony));

        if (Mathf.Min(natureHarmony, techHarmony) >= harmonyGoal)
        {
            Pause();
            creditsHandler.ActivateCredits();
        }
    }

    public int GetHarmonyGoal()
    {
        return harmonyGoal;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                pauseMenuUI.SetActive(false);

                Resume();
            }
            else
            {
                pauseMenuUI.SetActive(true);

                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetBuildMode(!gameIsInBuildMode);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        gameIsPaused = false;

        PlayerManager.Instance.Resume();

        SetCursorLock();
    }

    public void Pause()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;

        PlayerManager.Instance.Pause();

        SetCursorLock();
    }

    void SetBuildMode(bool value)
    {
        gameIsInBuildMode = !gameIsInBuildMode;
        OnBuildModeChanged?.Invoke(this, gameIsInBuildMode);
        SetCursorLock();
    }

    void SetCursorLock()
    {
        bool cursorIsUnlocked = (gameIsInBuildMode || gameIsPaused);
        Cursor.visible = cursorIsUnlocked;
        Cursor.lockState = cursorIsUnlocked ? CursorLockMode.None : CursorLockMode.Locked;
    }
}