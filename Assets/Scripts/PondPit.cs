using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondPit : DualObject
{
    // variant of DualObject class to handle particle effects on Pond/Pit objects

    [SerializeField] ParticleSystem natureParticleSystem;
    [SerializeField] ParticleSystem techParticleSystem;

    void Start()
    {
        SwapObjectOpacity(0f);
    }

    public override void SetGameObjectsActive()
    {
        natureWorldObject.SetActive(WorldSwap.Instance.GetIsInNatureWorld());
        techWorldObject.SetActive(!WorldSwap.Instance.GetIsInNatureWorld());
    }

    public override void SwapObjectOpacity(float materialChangeDuration)
    {
        ParticleSystem oldParticleSystem = WorldSwap.Instance.GetIsInNatureWorld() ? techParticleSystem : natureParticleSystem;
        ParticleSystem newParticleSystem = !WorldSwap.Instance.GetIsInNatureWorld() ? techParticleSystem : natureParticleSystem;

        oldParticleSystem.Stop();
        StartCoroutine(StartNewParticleSystem(newParticleSystem, materialChangeDuration));

        SetGameObjectsActive();
    }

    IEnumerator StartNewParticleSystem(ParticleSystem newParticleSystem, float materialChangeDuration)
    {
        yield return new WaitForSeconds(materialChangeDuration / 2f);
        newParticleSystem.Play();
    }
}
