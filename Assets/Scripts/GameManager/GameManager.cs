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
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public event EventHandler<HarmonyPair> OnHarmonyChanged;
    public int natureHarmony;
    public int techHarmony;

    //public GameObject crosshair;
    
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
            if (GameIsPaused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        //crosshair.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        GameIsPaused = false;


    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        //crosshair.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;


    }
}