using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HarmonyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI natureHarmonyCounter;
    [SerializeField] TextMeshProUGUI techHarmonyCounter;
    [SerializeField] TextMeshProUGUI totalHarmonyCounter;

    int displayNatureHarmony = 0;
    int displayTechHarmony = 0;

    int realNatureHarmony;
    int realTechHarmony;

    [SerializeField] float displayTextUpdatePerSecond = 20f;

    void Start()
    {
        GameManager.Instance.OnHarmonyChanged += UpdateHarmonyValues;
    }

    void UpdateHarmonyValues(object sender, HarmonyPair harmonyPair)
    {
        if (harmonyPair.natureHarmony == 0 && harmonyPair.techHarmony == 0) return;
        realNatureHarmony = harmonyPair.natureHarmony;
        realTechHarmony = harmonyPair.techHarmony;
    }

    void UpdateDisplayValues()
    {
        if (displayNatureHarmony < realNatureHarmony)
        {
            displayNatureHarmony = (int)Mathf.Floor(Mathf.Min(displayNatureHarmony + ((byte)(Time.deltaTime * displayTextUpdatePerSecond)), realNatureHarmony));
        }
        if (displayTechHarmony < realTechHarmony)
        {
            displayTechHarmony = (int)Mathf.Floor(Mathf.Min(displayTechHarmony + ((byte)(Time.deltaTime * displayTextUpdatePerSecond)), realTechHarmony));
        }

        UpdateUIText();
    }

    void UpdateUIText()
    {
        natureHarmonyCounter.text = displayNatureHarmony.ToString();
        techHarmonyCounter.text = displayTechHarmony.ToString();
        totalHarmonyCounter.text = Mathf.Min(displayNatureHarmony, displayTechHarmony).ToString();
    }

    void Update()
    {
        if (displayNatureHarmony == realNatureHarmony && displayTechHarmony == realTechHarmony) return;
        UpdateDisplayValues();
    }
}
