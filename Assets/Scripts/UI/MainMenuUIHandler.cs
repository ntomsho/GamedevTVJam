using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;


    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);


        Time.timeScale = 1.0f;
    }

    private void StartGame()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
