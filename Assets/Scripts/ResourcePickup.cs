using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    [SerializeField] ResourceType resourceType;
    [SerializeField] int resourceValue = 1;
    bool pickedUp;
    float timeToTweenToPlayer = 1f;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.name == "Player" && !pickedUp) // TODO: Update this to final transform name for player object
        {
            pickedUp = true;
            StartCoroutine(TweenToPlayer(collision.transform));
        }
    }

    IEnumerator TweenToPlayer(Transform playerTransform)
    {
        float timeElapsed = 0f;
        transform.localScale = transform.localScale * 1.2f;
        while (timeElapsed < timeToTweenToPlayer)
        {
            transform.localScale = transform.localScale * 0.99f;
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, timeElapsed / timeToTweenToPlayer);
            yield return null;
        }
        playerTransform.gameObject.GetComponent<Inventory>().AddResource(resourceType, resourceValue);
        Destroy(gameObject);
    }
}
