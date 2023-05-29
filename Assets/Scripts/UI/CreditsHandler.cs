using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsHandler : MonoBehaviour
{
    public void ActivateCredits()
    {
        gameObject.SetActive(true);

        // If game has been won, turn on winning display, effects
        if (GameIsWon())
        {
            SoundManager.Instance.PlayCreditsSound(PlayerManager.Instance.transform.position);
        }
    }

    bool GameIsWon()
    {
        return Mathf.Min(GameManager.Instance.natureHarmony, GameManager.Instance.techHarmony) >= GameManager.Instance.GetHarmonyGoal();
    }

    void CloseCredits()
    {
        gameObject.SetActive(false);
        GameManager.Instance.Resume();
    }

    void Update()
    {
        if (gameObject.activeSelf && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            CloseCredits();
        }
    }
}
