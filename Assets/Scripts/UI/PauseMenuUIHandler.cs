using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUIHandler : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;


    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void ResumeGame()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void MainMenu()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
