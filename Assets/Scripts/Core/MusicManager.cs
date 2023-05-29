using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MAIN_MUSIC_VOLUME = "MainMusicVolume";

    public static MusicManager Instance;

    private AudioSource audioSource;

    private float volume = 0.25f;

    private void Awake()
    {
        Instance = this; 
        
        audioSource = GetComponent<AudioSource>();

        SetVolume(volume);

    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        audioSource.volume = volume;
    }

    public float GetVolume() { return audioSource.volume; }
}
