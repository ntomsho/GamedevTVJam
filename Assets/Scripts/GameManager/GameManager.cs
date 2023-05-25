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

    }

    public void GainHarmony(int natureHarmonyToAdd, int techHarmonyToAdd)
    {
        natureHarmony += natureHarmonyToAdd;
        techHarmony += techHarmonyToAdd;

        OnHarmonyChanged?.Invoke(this, new HarmonyPair(natureHarmony, techHarmony));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {

        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);

        //crosshair.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        //crosshair.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    void SetBuildMode(bool value)
    {
        gameIsInBuildMode = !gameIsInBuildMode;
        OnBuildModeChanged?.Invoke(this, gameIsInBuildMode);
    }
}