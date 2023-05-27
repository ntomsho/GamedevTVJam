using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : BaseInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject fishPickupPrefab;
    //[SerializeField] float interactDuration = 25f;
    WorldType worldType = WorldType.Nature;

    public override void Interact(CharacterInteraction character)
    {
        Fish();
    }


    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Fish()
    {
        Instantiate(fishPickupPrefab, transform.position, Quaternion.identity);
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
