using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrowOverTime
{
    // Get the interval, in seconds, before calling the object's Grow()
    public float GetTimeToGrow();

    // Check if the object is grown (i.e. able to be harvested)
    public bool GetIsGrown();

    // Perform what's needed for the object to grow
    public void Grow();
}
