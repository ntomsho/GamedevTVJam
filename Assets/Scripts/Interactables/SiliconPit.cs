using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliconPit : BaseInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject siliconPickupPrefab;
    //[SerializeField] float interactDuration = 25f;
    WorldType worldType = WorldType.Technology;

    public  override void Interact(CharacterInteraction character)
    {
        Mine();
    }


    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Mine()
    {
        Instantiate(siliconPickupPrefab, transform.position, Quaternion.identity);
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
