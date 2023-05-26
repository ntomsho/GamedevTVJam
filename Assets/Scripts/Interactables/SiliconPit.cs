using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliconPit : MonoBehaviour, IInteractable, IDualObjectChild
{
    [SerializeField] Renderer objectRenderer;
    [SerializeField] DualObject dualObjectParent;
    [SerializeField] Outline highlightOutline;

    [SerializeField] Animation playerAnimation;
    [SerializeField] GameObject siliconPickupPrefab;
    [SerializeField] float interactDuration = 25f;
    WorldType worldType = WorldType.Technology;

    public void Interact(CharacterInteraction character)
    {
        Mine();
    }

    public float GetTimeToInteract()
    {
        return interactDuration;
    }

    public void SetHighlight(bool value)
    {
        Debug.Log(value);
        highlightOutline.enabled = value;
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
