using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InteractableScriptableObject", order = 2)]

public class InteractableSO : ScriptableObject
{
    public string Name;
    public string MainActionName;
    public string SecondaryActionName;

    public float TimeToInteract;
}
