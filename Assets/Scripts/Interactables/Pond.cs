using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : BaseInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] PondPit dualObjectParent;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject fishPickupPrefab;
    //[SerializeField] float interactDuration = 25f;
    WorldType worldType = WorldType.Nature;

    public override void Interact(CharacterInteraction character)
    {
        Fish(character.transform);
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Fish(Transform playerTransform)
    {
        Instantiate(fishPickupPrefab, FishDropPosition(playerTransform), Quaternion.identity);
    }

    Vector3 FishDropPosition(Transform playerTransform)
    {
        return (playerTransform.position - (playerTransform.position - transform.position).normalized);
    }

    public WorldType GetWorldType()
    {
        return worldType;
    }

    public void DestroyDualObject()
    {
        return; //Can't be destroyed;
    }
}
