using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private AudioSource audioSource;
    private float volume = 1.0f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayerManager.Instance.GetCharacterInteraction().OnInteractionStarted += SoundManager_OnInteractionStarted;
        WorldSwap.Instance.OnWorldSwap += Instance_OnWorldSwap;
        ResourcePickup.OnAnyResourcePickup += ResourcePickup_OnAnyResourcePickup;
        BuildingManager.OnAnyBuildingPlaced += BuildingManager_OnAnyBuildingPlaced;
    }

    private void BuildingManager_OnAnyBuildingPlaced(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.buildPlaced, PlayerManager.Instance.transform.position);
    }

    private void ResourcePickup_OnAnyResourcePickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.playerItemGrab, PlayerManager.Instance.transform.position);
    }

    private void Instance_OnWorldSwap(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.portalSwitch, PlayerManager.Instance.transform.position);
    }

    private void SoundManager_OnInteractionStarted(object sender, CharacterInteraction.OnInteractionStartedEventArgs e)
    {
        PlaySound(audioClipRefsSO.uiCrafting[0], PlayerManager.Instance.transform.position);
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
    public void PlayCreditsSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipRefsSO.credits, position, volumeMultiplier * volume);

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
