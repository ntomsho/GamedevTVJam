using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliconPit : BaseInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] PondPit dualObjectParent;
    [SerializeField] Transform pickupDestinationTransform;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject siliconPickupPrefab;
    //[SerializeField] float interactDuration = 25f;
    WorldType worldType = WorldType.Technology;

    public  override void Interact(CharacterInteraction character)
    {
        Mine();
    }

    public override void SetHighlight(bool value)
    {
        return;
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    void Mine()
    {
        Instantiate(siliconPickupPrefab, transform.position, Quaternion.identity);
    }

    Vector3 SiliconDropPosition(Transform playerTransform)
    {
        float x = pickupDestinationTransform.position.x + Random.Range(-0.2f, 0.2f);
        float z = pickupDestinationTransform.position.z + Random.Range(-0.2f, 0.2f);
        return new Vector3(x, pickupDestinationTransform.position.y, z);
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
