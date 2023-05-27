using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {

    }


    //private void DeliveryManager_OnWaitingRecipeSuccess(object sender, System.EventArgs e)
    //{
    //    DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
    //    PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    //}

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipRefsSO.playerFootstep, position, volumeMultiplier * volume);

    }

    //public void PlayCountdownSound()
    //{
    //    PlaySound(audioClipRefsSO.warning, Vector3.zero);

    //}


    //public void PlayWarningSound(Vector3 position)
    //{
    //    PlaySound(audioClipRefsSO.warning, position);

    //}

    public void ChangeVolume(float volumeChangeTo)
    {
        volume = volumeChangeTo;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volumeChangeTo);
        PlayerPrefs.Save();
    }

    public float GetVolume() { return volume; }
}
