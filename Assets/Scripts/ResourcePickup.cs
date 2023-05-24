using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] ResourceType resourceType;
    [SerializeField] int resourceValue = 1;
    bool pickedUp;
    float timeToTweenToPlayer = 5f;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.name == "PlayerArmature" && !pickedUp) // TODO: Update this to final transform name for player object
        {
            pickedUp = true;
            StopParticles();
            StartCoroutine(TweenToPlayer(collision.transform));
        }
    }

    void StopParticles()
    {
        particles.Stop();
        particles.transform.parent = null;
    }

    IEnumerator TweenToPlayer(Transform playerTransform)
    {
        float timeElapsed = 0f;
        transform.localScale = transform.localScale * 1.2f;
        while (timeElapsed < timeToTweenToPlayer)
        {
            transform.localScale = transform.localScale * 0.99f;
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, timeElapsed / timeToTweenToPlayer);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        playerTransform.parent.GetComponent<Inventory>().AddResource(resourceType, resourceValue);
        Debug.Log($"picked up {resourceValue} {resourceType}");
        
    }
}
