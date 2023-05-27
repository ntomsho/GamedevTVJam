using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] uiSelector;
    public AudioClip[] uiCrafting;
    public AudioClip[] uiClick;
    public AudioClip[] uiAchievement;
    
    public AudioClip[] buildRemove;
    
    public AudioClip portalSwitch;

    public AudioClip playerCraft;
    public AudioClip playerStateSwitch;
    public AudioClip[] playerFootstep;
}
